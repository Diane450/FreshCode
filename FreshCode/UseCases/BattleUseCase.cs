using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.UseCases
{
    public class BattleUseCase
    {
        private readonly IBattleRepository _battleRepository;
        private readonly IPetsRepository _petRepository;


        public BattleUseCase(IBattleRepository battleRepository, IPetsRepository petsRepository)
        {
            _battleRepository = battleRepository;
            _petRepository = petsRepository;
        }
        public async Task<PetDTO> FindOppenont(long userId)
        {
            Pet pet = await _petRepository.GetPetByUserId(userId);
            IQueryable<Pet> opponents = _battleRepository.GetPetOpponents(pet.LevelId);
            Random random = new Random();

            int index = random.Next(0, opponents.Count() + 1);
            var opponentsList = await opponents.ToListAsync();
            var selectedOpponent = opponentsList[index];
            return PetMapper.ToDto(selectedOpponent);
        }
    }
}
