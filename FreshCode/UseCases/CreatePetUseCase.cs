using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using System.Runtime.InteropServices;

namespace FreshCode.UseCases
{
    public class CreatePetUseCase(IEyesRepository eyesRepository,
        IUserRepository userRepository,
        IPetsRepository petRepository,
        IBodyRepository bodyRepository,
        IBaseRepository baseRepository)

    {
        private readonly IEyesRepository _eyesRepository = eyesRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetsRepository _petRepository = petRepository;
        private readonly IBodyRepository _bodyRepository = bodyRepository;
        private readonly IBaseRepository _baseRepository= baseRepository;


        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _eyesRepository.GetEyesAsync();
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _bodyRepository.GetBodiesAsync();
        }

        public async Task<PetDTO> CreatePetAsync(CreatePetRequest request, long userId)
        {
            Pet pet = _petRepository.CreatePet(request, userId);

            await _baseRepository.AddAsync(pet);
            await _baseRepository.SaveChangesAsync();
            return PetMapper.ToDto(pet);
        }
    }
}
