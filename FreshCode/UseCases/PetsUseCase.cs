using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class PetsUseCase
    {
        private readonly IPetsRepository _petsRepository;
        

        public PetsUseCase(IPetsRepository petsRepository)
        {
            _petsRepository = petsRepository;
            
        }
        public async Task<Pet> GetPetByVkIdAsync(int VkId)
        {
            return await _petsRepository.GetPetInfoAsync(VkId);
        }

        public async Task<Pet> CreatePetAsync(CreatePetRequest request, string? vk_user_id)
        {
           return await _petsRepository.CreatePetAsync(request, vk_user_id);
        }

        public async Task<Pet> LevelUpAsync(Pet pet)
        {
            return await _petsRepository.LevelUpAsync(pet);
        }
    }
}
