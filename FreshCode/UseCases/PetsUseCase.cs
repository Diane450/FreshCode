using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class PetsUseCase(IPetsRepository petsRepository, IUserRepository userRepository)
    {
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<PetDTO> GetPetByIdAsync(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _petsRepository.GetPetByUserId(userId);
        }

        public async Task<PetDTO> LevelUpAsync(PetDTO petDTO)
        {
            Pet pet = await _petsRepository.GetPetById(petDTO.Id);

            pet.Level += 1;

            return await _petsRepository.LevelUpAsync(pet);
        }

        public async System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet)
        {
            await _petsRepository.ChangePetsArtifact(pet);
        }
    }
}
