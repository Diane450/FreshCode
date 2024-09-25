using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class BannerUseCase
    {
        private readonly IBanerRepository _banerRepository;

        public BannerUseCase(IBanerRepository banerRepository)
        {
            _banerRepository = banerRepository;
        }
        public async Task<BanerDTO> GetBannerById(long bannerId)
        {
            Banner banner = await _banerRepository.GetBannerById(bannerId);

            return BannerMapper.ToDTO(banner);
        }
    }
}
