using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;

namespace FreshCode.UseCases
{
    public class CreatePetUseCase(ICreatePetRepository createPetRepository)
    {
        private readonly ICreatePetRepository _createPetRepository = createPetRepository;

        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _createPetRepository.GetEyesAsync();
        }
    }
}
