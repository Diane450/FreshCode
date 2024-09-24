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
        IBaseRepository baseRepository,
        IFoodRepository foodRepository)
    {
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly TransactionRepository _transactionRepository = transactionRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;
        private readonly IFoodRepository _foodRepository = foodRepository;


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

        public async Task<ActionResult<PetDTO>> IncreaseStat(long userId, IncreaseStatRequest request)
        {
            //TODO: обновить среднюю силу питомца
            User user = await _userRepository.GetUserById(userId);
            Pet pet = await _petsRepository.GetPetById(request.PetId);

            user.StatPoints -= 1;

            CheckStatCount(user);

            pet.IncreaseStat(request.Characteristic);

            _baseRepository.Update(pet);
            await _baseRepository.SaveChangesAsync();

            return PetMapper.ToDto(pet);
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

        public async Task<PetDTO> CreatePetAsync(CreatePetRequest request, long userId)
        {
            Pet pet = _petsRepository.CreatePet(request, userId);

            await _baseRepository.AddAsync(pet);
            await _baseRepository.SaveChangesAsync();

            return PetMapper.ToDto(pet);
        }

        public async System.Threading.Tasks.Task Feed(long userId, FeedRequest request)
        {
            UserFood userFood = _userRepository.GetUserFood(userId)
                .First(uf => uf.FoodId == request.FoodId);

            if (userFood.Count == 0)
            {
                throw new Exception("User does not have this food");
            }

            userFood.Count -= 1;

            if (userFood.Count == 0)
            {
                _baseRepository.Remove(userFood);
            }
            Food food = await _foodRepository.GetFoodById(request.FoodId);

            Pet pet = await _petsRepository.GetPetById(request.PetId);
            pet.Feed(food.FoodBonuses.Select(f => f.Bonus).ToList());

            //foreach (var foodBonus in food.FoodBonuses)
            //{
            //    switch (foodBonus.Bonus.Characteristic.Characteristic1)
            //    {
            //        case ("Критический урон"):
            //            pet.CurrentCriticalChance += foodBonus.Bonus.Value;
            //            break;
            //        case ("Критический шанс"):
            //            pet.CurrentCriticalChance += foodBonus.Bonus.Value;
            //            break;
            //        case ("Защита"):
            //            pet.CurrentDefence += foodBonus.Bonus.Value;
            //            break;
            //        case ("Здоровье"):
            //            pet.CurrentHealth += foodBonus.Bonus.Value;
            //            break;
            //        case ("Сила"):
            //            pet.CurrentStrength += foodBonus.Bonus.Value;
            //            break;
            //        case ("Сон"):
            //            pet.SleepNeed += foodBonus.Bonus.Value;
            //            break;
            //        case ("Питание"):
            //            pet.FeedNeed += foodBonus.Bonus.Value;
            //            break;
            //        default:
            //            throw new Exception($"Characteristic {foodBonus.Bonus.Characteristic.Characteristic1} does not exist");
            //    }
            //}
            _baseRepository.Update(pet);
            await _baseRepository.SaveChangesAsync();
        }
    }
}
