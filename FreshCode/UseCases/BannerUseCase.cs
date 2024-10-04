using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Responses;
using FreshCode.Services;
using System;

namespace FreshCode.UseCases
{
    public class BannerUseCase
    {
        private readonly IBanerRepository _banerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IArtifactRepository _artifactRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly ArtifactDropService _artifactDropService;

        public BannerUseCase(IBanerRepository banerRepository,
            IUserRepository userRepository,
            IArtifactRepository artifactRepository,
            IBaseRepository baseRepository,
            ArtifactDropService artifactDropService)
        {
            _banerRepository = banerRepository;
            _userRepository = userRepository;
            _artifactRepository = artifactRepository;
            _baseRepository = baseRepository;
            _artifactDropService = artifactDropService;
        }
        public async Task<BanerDTO> GetBannerById(long bannerId)
        {
            Banner banner = await _banerRepository.GetBannerById(bannerId);
            return BannerMapper.ToDTO(banner);
        }

        public async Task<DropArtifactResponse> GetArtifact(long userId, long bannerId)
        {
            User user = await _userRepository.GetUserById(userId);

            Banner banner = await _banerRepository.GetBannerById(bannerId);

            List<ArtifactHistory> history = _userRepository.GetArtifactHistory(userId, bannerId).ToList();

            IQueryable<BannerItem> bannerItems = _banerRepository.GetAllBannerItems();

            var bannerItem = _artifactDropService.GetArtifact(banner, history, bannerItems);
            
            var response = new DropArtifactResponse();
            
            if (history.Where(h => h.ArtifactId == bannerItem.ArtifactId).Count() >= 1)
            {//TODO: сколько бабок за повторюшку давать
                user.Money += 100;
                response.Money = 100;
                response.IsArtifactOwnedPreviously = true;
            }
            else
            {
                user.UserArtifacts.Add(new UserArtifact
                {
                    UserId = userId,
                    ArtifactId = bannerItem.ArtifactId,
                });
            }
            response.ArtifactId = bannerItem.ArtifactId;

            ArtifactHistory artifactHistory = new ArtifactHistory()
            {
                ArtifactId = bannerItem.ArtifactId,
                UserId = userId,
                BannerId = bannerId,
                GotAt = DateTime.UtcNow
            };
            //await _baseRepository.AddAsync(artifactHistory);
            //await _baseRepository.SaveChangesAsync();
            return response;
        }
    }
}