using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class CreatePetUseCase(ICreatePetRepository createPetRepository)
    {
        private readonly ICreatePetRepository _createPetRepository = createPetRepository;

        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _createPetRepository.GetEyesAsync();
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _createPetRepository.GetBodiesAsync();
        }
        public async Task<PetDTO> CreatePetAsync(CreatePetRequest request, string? vk_user_id)
        {
            Pet pet = await _createPetRepository.CreatePetAsync(request, vk_user_id);
            return PetMapper.ToDto(pet);
        }
    }
}
