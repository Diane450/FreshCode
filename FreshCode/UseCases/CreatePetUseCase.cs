using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.EF_Repositories;
using FreshCode.Requests;
using System.Runtime.InteropServices;

namespace FreshCode.UseCases
{
    public class CreatePetUseCase(IEyesRepository eyesRepository,
        IUserRepository userRepository,
        IPetsRepository petRepository,
        IBodyRepository bodyRepository)

    {
        private readonly IEyesRepository _eyesRepository = eyesRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetsRepository _petRepository = petRepository;
        private readonly IBodyRepository _bodyRepository = bodyRepository;


        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _eyesRepository.GetEyesAsync();
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _bodyRepository.GetBodiesAsync();
        }

        public async Task<PetDTO> CreatePetAsync(CreatePetRequest request, string? vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);

            Pet pet = await _petRepository.CreatePetAsync(request, userId);
            return PetMapper.ToDto(pet);
        }
    }
}
