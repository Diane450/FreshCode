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
        IBodyRepository bodyRepository)

    {
        private readonly IEyesRepository _eyesRepository = eyesRepository;
        private readonly IBodyRepository _bodyRepository = bodyRepository;


        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _eyesRepository.GetEyesAsync();
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _bodyRepository.GetBodiesAsync();
        }
    }
}
