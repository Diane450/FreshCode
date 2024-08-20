using FreshCode.DbModels;
using FreshCode.Exceptions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.AspNetCore.Mvc;

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
            //TODO: изменить логику повышения стата
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

        public async Task<ActionResult<PetDTO>> IncreaseStat(string? vk_user_id, IncreaseStatRequest statRequest)
        {
            //TODO: обновить среднюю силу питомца
            User user = await _userRepository.GetUserByVkId(vk_user_id);
            Pet pet = await _petsRepository.GetPetById(statRequest.PetDTO.Id);
            
            
            user.StatPoints -= 1;

            CheckStatCount(user);

            statRequest.Characteristic.IncreaseStat(pet);

            switch (statRequest.Characteristic)
            {
                case CharacteristicType.Health:
                    statRequest.PetDTO.CurrentHealth = pet.CurrentHealth;
                    break;
                case CharacteristicType.Defence:
                    statRequest.PetDTO.CurrentDefence = pet.CurrentDefence;
                    break;
                case CharacteristicType.Strength:
                    statRequest.PetDTO.CurrentStrength = pet.CurrentStrength;
                    break;
                case CharacteristicType.CriticalDamage:
                    statRequest.PetDTO.CurrentCriticalDamage = pet.CurrentCriticalDamage;
                    break;
                case CharacteristicType.CriticalChance:
                    statRequest.PetDTO.CurrentCriticalChance = pet.CurrentCriticalChance;
                    break;
            }

            _petsRepository.UpdateAsync(pet);
            await _petsRepository.SaveShangesAsync();
            return statRequest.PetDTO;
        }

        private void CheckStatCount(User user)
        {
            if (user.StatPoints < 0)
            {
                throw new InsufficientFundsException();
            }
        }

        public async System.Threading.Tasks.Task SetArtifact(SetArtifactRequest setArtifactRequest)
        {
            Pet pet = await _petsRepository.GetPetById(setArtifactRequest.Pet.Id);

            pet.ChangeArtifact(setArtifactRequest.Artifact);

            await _petsRepository.SaveShangesAsync();
        }
    }
}
