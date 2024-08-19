using FreshCode.DbModels;
using FreshCode.Exceptions;
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
            Pet pet = await _petsRepository.GetPetByUserId(userId);
            return PetMapper.ToDto(pet);
        }

        public async Task<PetDTO> LevelUpAsync(PetDTO petDTO)
        {
            Pet pet = await _petsRepository.GetPetById(petDTO.Id);

            pet.Points = 0;

            pet.Level = await _petsRepository.GelLevelValues(pet.LevelId + 1);
            _petsRepository.UpdateAsync(pet);
            await _petsRepository.SaveShangesAsync();
            return PetMapper.ToDto(pet);
        }

        public async System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet)
        {
            await _petsRepository.ChangePetsArtifact(pet);
        }

        public async Task<PetDTO> IncreaseHealth(string vk_user_id, PetDTO petDTO)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);
            Pet pet = await _petsRepository.GetPetById(petDTO.Id);

            user.StatPoints -= 1;

            if (user.StatPoints < 0)
            {
                throw new InsufficientFundsException();
            }

            pet.CurrentHealth *= (int)pet.Level.EnhancementCoefficient;

            if (pet.CurrentHealth > pet.Level.MaxHealth)
            {
                throw new ArgumentException("Достигнуто максимальное значение свойства");
            }

            petDTO.CurrentHealth = pet.CurrentHealth;
            _petsRepository.UpdateAsync(pet);
            await _petsRepository.SaveShangesAsync();
            return petDTO;
        }
    }
}
