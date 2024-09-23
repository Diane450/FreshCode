using FreshCode.DbModels;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.UseCases
{
    public class BlogUseCase(IBlogRepository blogRepository,
        ICommentRepository commentRepository,
        IBaseRepository baseRepository,
        TransactionRepository transactionRepository)
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async Task<PagedList<PostDTO>> GetPosts(QueryParameters parameters)
        {
            try
            {
                IQueryable<Post> posts = _blogRepository.GetAllPosts();

                posts = posts.Sort(parameters.SortBy, parameters.SortDescending);

                posts = posts.Filter(parameters.FilterBy, parameters.FilterValue);

                int totalCount = await posts.CountAsync();

                posts = posts.Paginate(parameters.Page, parameters.PageSize);


                List<PostDTO> postsDto = PostMapper.ToDTO(posts.ToList());

                return new PagedList<PostDTO>(postsDto, parameters.Page, parameters.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PostDTO> CreatePost(CreatePostRequest request, long userId)
        {
            using var transaction = _transactionRepository.BeginTransaction();
            try
            {
                Post post = new()
                {
                    Title = request.Title,
                    CreatedAt = DateTime.UtcNow,
                    TagId = request.TagId,
                    UserId = userId,
                };

                await _baseRepository.AddAsync(post);
                await _baseRepository.SaveChangesAsync();

                foreach (var block in request.PostBlock)
                {
                    PostBlock postBlock = new PostBlock
                    {
                        Content = block.Content,
                        ContentTypeId = block.ContentTypeId,
                        Index = block.Index,
                        PostId = post.Id,
                    };
                    await _baseRepository.AddAsync(postBlock);
                }
                await _baseRepository.SaveChangesAsync();
                transaction.Commit();
                return PostMapper.ToDTO(post);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedList<CommentDTO>> GetCommentsByPostId(QueryParameters parameters, long blogId)
        {
            IQueryable<PostComment> comments = _commentRepository.GetCommentsByPostId(blogId);

            comments = comments.Sort(parameters.SortBy, parameters.SortDescending);
            
            int totalCount = await comments.CountAsync();

            comments = comments.Paginate(parameters.Page, parameters.PageSize);


            List<CommentDTO> commentDto = CommentMapper.ToDTO(comments.ToList());

            return new PagedList<CommentDTO>(commentDto, parameters.Page, parameters.PageSize, totalCount);
        }

        public async System.Threading.Tasks.Task DeletePost(long postId, long userId)
        {
            var post = await _blogRepository.GetPostById(postId);
            if (post.UserId != userId)
            {
                throw new Exception("User cannot delete this post");
            }
            post.DeletedAt = DateTime.UtcNow;

            await _baseRepository.SaveChangesAsync();
        }

        public async Task<CommentDTO> CreateComment(CreateCommentRequest request, long postId)
        {
            PostComment postComment = new PostComment()
            {
                Comment = request.Comment,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                PostId = postId
            };

            await _baseRepository.AddAsync(postComment);
            await _baseRepository.SaveChangesAsync();
            return PostCommentMapper.ToDto(postComment);
        }

        public async Task<CommentDTO> EditComment(string newText, long commentId)
        {
            PostComment comment = await _commentRepository.GetCommentById(commentId);
            comment.Comment = newText;
            comment.UpdatedAt = DateTime.UtcNow;
            await _baseRepository.SaveChangesAsync();

            return PostCommentMapper.ToDto(comment);
        }

        public async Task<int> AddReactionToPost(long userId, long postId, bool reactionValue)
        {
            PostRating postRating = new PostRating()
            {
                UserId = userId,
                PostId = postId,
                Rating = reactionValue
            };
            await _baseRepository.AddAsync(postRating);
            await _baseRepository.SaveChangesAsync();

            IQueryable<PostRating> rating = _blogRepository.GetPostReactions(postId);

            if (reactionValue)
            {
                return await rating.Where(pr => pr.Rating == true).CountAsync();
            }
            return await rating.Where(pr => pr.Rating == false).CountAsync();
        }

        public async Task<PostDTO> EditPost(List<PostBlockDTO> blocks, long postId)
        {
            if (blocks.Count == 0)
            {
                throw new ArgumentException("Post blocks cannot be null");
            }
            List<PostBlock> existingPostBlocks = await _blogRepository.GetPostBlocks(postId);

            List<long> existingPostBlocksId = existingPostBlocks.Select(x => x.Id).ToList();

            foreach (PostBlockDTO block in blocks)
            {
                if (block.Id != 0)
                {
                    var existingPostBlock = existingPostBlocks.Find(p => p.Id == block.Id);
                    if (existingPostBlock == null)
                    {
                        throw new ArgumentException("Incorrect post Id");
                    }
                    existingPostBlock.Content = block.Content;
                    existingPostBlock.Index = block.Index;
                    existingPostBlock.ContentTypeId = block.ContentTypeId;

                }
                else
                {
                    var newBlock = new PostBlock()
                    {
                        PostId = postId,
                        Content = block.Content,
                        Index = block.Index,
                        ContentTypeId = block.ContentTypeId
                    };
                    await _baseRepository.AddAsync(newBlock);
                }
            }

            var blockIdsToRemove = existingPostBlocksId.Except(blocks.Where(b => b.Id != 0).Select(b => b.Id)).ToList();

            if (blockIdsToRemove.Any())
            {
                var blocksToRemove = existingPostBlocks.Where(b => blockIdsToRemove.Contains(b.Id)).ToList();
                _baseRepository.RemoveRange(blocksToRemove);
            }
            await _baseRepository.SaveChangesAsync();

            Post post = await _blogRepository.GetPostById(postId);
            return PostMapper.ToDTO(post);
        }
    }
}
