using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using FreshCode.Responses;
using FreshCode.Services;
using System;

namespace FreshCode.UseCases
{
    public class BannerUseCase(IBanerRepository banerRepository,
        IUserRepository userRepository,
        IArtifactRepository artifactRepository,
        IBaseRepository baseRepository,
        ArtifactDropService artifactDropService,
        TransactionRepository transactionRepository)
    {
        private readonly IBanerRepository _banerRepository = banerRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IArtifactRepository _artifactRepository = artifactRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;
        private readonly ArtifactDropService _artifactDropService = artifactDropService;
        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async Task<BanerDTO> GetBannerById(long bannerId)
        {
            Banner banner = await _banerRepository.GetBannerById(bannerId);
            return BannerMapper.ToDTO(banner);
        }

        public async Task<DropArtifactResponse> GetArtifact(long userId, WishRequest wishRequest)
        {
            User user = await _userRepository.GetUserById(userId);

            HasEnoughFates(user, wishRequest.WishAmount);

            Banner banner = await _banerRepository.GetBannerById(wishRequest.BannerId);
            List<ArtifactHistory> history = _userRepository.GetArtifactHistory(userId, wishRequest.BannerId).ToList();
            IQueryable<BannerItem> bannerItems = _banerRepository.GetAllBannerItems();

            var result = new DropArtifactResponse
            {
                artifacts = new List<ArtifactResponse>()
            };

            using var transaction = _transactionRepository.BeginTransaction();

            try
            {
                for (int i = 0; i < wishRequest.WishAmount; i++)
                {
                    var bannerItem = await GetBannerItemWithArtifact(banner, history, bannerItems);
                    var artifactResponse = CreateArtifactResponse(bannerItem);
                    result.artifacts.Add(artifactResponse);

                    if (!await IsUserHadArtifact(userId, bannerItem.ArtifactId))
                    {
                        AddNewUserArtifact(user, bannerItem);
                        artifactResponse.IsArtifactOwnedPreviously = false;
                    }
                    else
                    {
                        HandleDuplicateArtifact(user, result, artifactResponse);
                    }

                    await RecordArtifactHistory(userId, wishRequest.BannerId, bannerItem.ArtifactId, history);
                    await _baseRepository.SaveChangesAsync();
                }
                DeductFates(user, wishRequest.WishAmount, result);

                await _baseRepository.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("Не удалось совершить крутку баннера");
            }
            result.TotalMoney = user.Money;
            return result;
        }

        private void HasEnoughFates(User user, int wishAmount)
        {
            if (user.FatesCount < wishAmount)
            {
                throw new InvalidOperationException("Недостаточно круток");
            }
        }
        
        private async Task<BannerItem> GetBannerItemWithArtifact(Banner banner, List<ArtifactHistory> history, IQueryable<BannerItem> bannerItems)
        {
            var bannerItem = _artifactDropService.GetArtifact(banner, history, bannerItems);
            bannerItem.Artifact = await _artifactRepository.GetArtifactById(bannerItem.ArtifactId);
            return bannerItem;
        }

        private ArtifactResponse CreateArtifactResponse(BannerItem bannerItem)
        {
            return new ArtifactResponse
            {
                ArtifactId = bannerItem.ArtifactId,
                IsArtifactOwnedPreviously = false,
                X = bannerItem.Artifact.X,
                Y = bannerItem.Artifact.Y,
                Rarity = bannerItem.Artifact.Rarity.Rarity1
            };
        }

        private void AddNewUserArtifact(User user, BannerItem bannerItem)
        {
            user.UserArtifacts.Add(new UserArtifact
            {
                UserId = user.Id,
                ArtifactId = bannerItem.ArtifactId
            });
        }

        private void HandleDuplicateArtifact(User user, DropArtifactResponse result, ArtifactResponse artifactResponse)
        {
            const int duplicateMoneyReward = 100;

            artifactResponse.Money = duplicateMoneyReward;
            artifactResponse.IsArtifactOwnedPreviously = true;

            user.Money += duplicateMoneyReward;
        }

        private async System.Threading.Tasks.Task RecordArtifactHistory(long userId, long bannerId, long artifactId, List<ArtifactHistory> history)
        {
            var artifactHistory = new ArtifactHistory
            {
                ArtifactId = artifactId,
                UserId = userId,
                BannerId = bannerId,
                GotAt = DateTime.UtcNow
            };

            await _baseRepository.AddAsync(artifactHistory);
            history.Add(artifactHistory);
        }

        private void DeductFates(User user, int wishAmount, DropArtifactResponse result)
        {
            user.FatesCount -= wishAmount;
            result.TotalFates = user.FatesCount;
        }

        private async Task<bool> IsUserHadArtifact(long userId, long artifactId)
        {
            return await _userRepository.isArtifactAbsent(artifactId, userId);
        }
    }
}