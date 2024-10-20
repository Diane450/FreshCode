using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace FreshCode.UseCases
{
    public class PetPartsUseCase(IEyesRepository eyesRepository,
        IBodyRepository bodyRepository)

    {
        private readonly IEyesRepository _eyesRepository = eyesRepository;
        private readonly IBodyRepository _bodyRepository = bodyRepository;


        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            var eyesListDTO = await _eyesRepository.GetEyesAsync()
                .Select(e => new EyeDTO
                {
                    Id = e.Id,
                    X = e.X,
                    Y = e.Y,
                    // Включите все необходимые поля для DTO
                })
                .ToListAsync();

            return eyesListDTO;
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _bodyRepository.GetBodiesAsync()
                .Select(b => new BodyDTO
                {
                    Id = b.Id,
                    X = b.X,
                    Y = b.Y
                })
                .ToListAsync();
        }
    }
}
