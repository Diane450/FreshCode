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

        public async Task<PetDTO> LevelUpAsync(PetDTO pet)
        {
            return await _petsRepository.LevelUpAsync(pet);
        }

        public async System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet)
        {
            await _petsRepository.ChangePetsArtifact(pet);
        }

        public async System.Threading.Tasks.Task FeedAsync(FeedRequest request)
        {
            await _petsRepository.FeedAsync(request.Pet);
        }
    }
}
