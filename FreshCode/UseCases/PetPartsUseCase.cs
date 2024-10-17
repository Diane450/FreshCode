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
            IQueryable<Eye> eyes = _eyesRepository.GetEyesAsync();
            var eyesListDTO = eyes.Select(e => EyeMapper.ToDTO(e)).ToListAsync();
            return await eyesListDTO;
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            IQueryable<Body> bodies = _bodyRepository.GetBodiesAsync();
            var bodiesDTO = bodies.Select(b=>BodyMapper.ToDTO(b)).ToListAsync();
            return await bodiesDTO;
        }
    }
}
