using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class PetsUseCase(IPetsRepository petsRepository)
    {
        private readonly IPetsRepository _petsRepository = petsRepository;

        public async Task<PetDTO> GetPetByVkIdAsync(int VkId)
        {
            return await _petsRepository.GetPetInfoAsync(VkId);
        }

        public async Task<PetDTO> CreatePetAsync(CreatePetRequest request, string? vk_user_id)
        {
           Pet pet = await _petsRepository.CreatePetAsync(request, vk_user_id);
           return PetMapper.ToDto(pet);
        }

        public async Task<PetDTO> LevelUpAsync(PetDTO pet)
        {
            return await _petsRepository.LevelUpAsync(pet);
        }
    }
}
