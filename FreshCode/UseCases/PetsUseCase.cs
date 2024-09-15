using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Exceptions;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.UseCases
{
    public class PetsUseCase(IPetsRepository petsRepository,
        IUserRepository userRepository,
        TransactionRepository transactionRepository,
        IBaseRepository baseRepository)
    {
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly TransactionRepository _transactionRepository = transactionRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;


        public async Task<PetDTO> GetPetByUserIdAsync(long userId)
        {
            Pet pet = await _petsRepository.GetPetByUserId(userId);
            return PetMapper.ToDto(pet);
        }

        public async Task<PetDTO> LevelUpAsync(long petId)
        {
            Pet pet = await _petsRepository.GetPetById(petId);

            pet.Points = 0;

            pet.Level = await _petsRepository.GelLevelValues(pet.LevelId + 1);
            _baseRepository.Update(pet);
            await _baseRepository.SaveChangesAsync();
            return PetMapper.ToDto(pet);
        }

        public async Task<ActionResult<PetDTO>> IncreaseStat(long userId, IncreaseStatRequest statRequest)
        {
            //TODO: обновить среднюю силу питомца
            User user = await _userRepository.GetUserById(userId);
            Pet pet = await _petsRepository.GetPetById(statRequest.PetDTO.Id);
            
            user.StatPoints -= 1;

            CheckStatCount(user);

            pet.IncreaseStat(statRequest.Characteristic);

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

            _baseRepository.Update(pet);
            await _baseRepository.SaveChangesAsync();
            return statRequest.PetDTO;
        }

        private void CheckStatCount(User user)
        {
            if (user.StatPoints < 0)
            {
                throw new InsufficientFundsException();
            }
        }

        public async Task<PetDTO> SetArtifact(SetArtifactRequest setArtifactRequest)
        {
            Pet pet = await _petsRepository.GetPetById(setArtifactRequest.PetId);

            pet.AssignArtifact(setArtifactRequest.Artifact);

            await _baseRepository.SaveChangesAsync();
            return PetMapper.ToDto(pet);
        }

        public async Task<PetDTO> RemoveArtifact(RemoveArtifactRequest removeArtifactRequest)
        {
            Pet pet = await _petsRepository.GetPetById(removeArtifactRequest.PetId);
            pet.RemoveArtifact(removeArtifactRequest.ArtifactToRemove);
            return PetMapper.ToDto(pet);
        }
    }
}
