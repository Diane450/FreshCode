using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.Exceptions;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using FreshCode.Responses;
using FreshCode.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.UseCases
{
    public class PetsUseCase(IPetsRepository petsRepository,
        IUserRepository userRepository,
        TransactionRepository transactionRepository,
        IBaseRepository baseRepository,
        IFoodRepository foodRepository,
        IArtifactRepository artifactRepository,
        IArtifactService artifactService,
        IPetBonusManagerService bonusRepository)
    {
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly TransactionRepository _transactionRepository = transactionRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;
        private readonly IFoodRepository _foodRepository = foodRepository;
        private readonly IArtifactRepository _artifactRepository = artifactRepository;
        private readonly IArtifactService _artifactService = artifactService;
        private readonly IPetBonusManagerService _bonusRepository = bonusRepository;


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

            Artifact artifact = await _artifactRepository.GetArtifactById(setArtifactRequest.ArtifactId);

            _artifactService.AssignArtifact(pet, artifact);

            await _baseRepository.SaveChangesAsync();
            return PetMapper.ToDto(pet);
        }

        public async Task<PetDTO> RemoveArtifact(RemoveArtifactRequest removeArtifactRequest)
        {
            Pet pet = await _petsRepository.GetPetById(removeArtifactRequest.PetId);

            Artifact artifact = await _artifactRepository.GetArtifactById(removeArtifactRequest.ArtifactToRemoveId);

            _artifactService.RemoveArtifact(pet, artifact);

            await _baseRepository.SaveChangesAsync();
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
            IQueryable<PetFeedLog> log = _petsRepository.GetFeedPetLogLast5Minute(request.PetId);

            if (log.Count() >= 3)
            {
                throw new Exception("Питомец уже наелся!");
            }

            UserFood userFood = _userRepository.GetUserFood(userId)
                .FirstOrDefault(uf => uf.FoodId == request.FoodId);

            if (userFood is null || userFood.Count == 0)
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

            var currentStats = await GetPetStats(request.PetId);

            var currentBonuses = _petsRepository.GetPetBonuses(request.PetId)
                .Where(ub => ub.BonusTypeId == 2).ToList();

            var foodBonuses = food.FoodBonuses.ToList();

            for (int i = 0; i < foodBonuses.Count; i++)
            {
                if (foodBonuses[i].IsTemporary)
                {
                    if (foodBonuses[i].Bonus.Value > 0)
                    {
                        var bonus = currentBonuses.FirstOrDefault(c => c.Bonus.CharacteristicId == foodBonuses[i].Bonus.CharacteristicId);

                        if (bonus is null)
                        {
                            UserBonuse userBonuse = new UserBonuse
                            {
                                PetId = pet.Id,
                                BonusId = foodBonuses[i].BonusId,
                                CreatedAt = DateTime.UtcNow,
                                ExpiresAt = DateTime.UtcNow.AddSeconds(foodBonuses[i].Bonus.Duration),
                                BonusTypeId = 2
                            };
                            await _baseRepository.AddAsync(userBonuse);
                        }
                        else
                        {
                            bonus.BonusId = foodBonuses[i].BonusId;
                            bonus.CreatedAt = DateTime.UtcNow;
                            bonus.ExpiresAt = DateTime.UtcNow.AddSeconds(foodBonuses[i].Bonus.Duration);
                        }
                    }
                    else
                    {
                        UserBonuse userBonuse = new UserBonuse
                        {
                            PetId = pet.Id,
                            BonusId = foodBonuses[i].BonusId,
                            CreatedAt = DateTime.UtcNow,
                            ExpiresAt = DateTime.UtcNow.AddSeconds(foodBonuses[i].Bonus.Duration),
                            BonusTypeId = 2
                        };
                        await _baseRepository.AddAsync(userBonuse);
                    }
                }
                else
                {
                    await _bonusRepository.SetBonus(pet, foodBonuses[i].Bonus);
                }
            }

            PetFeedLog petFeedLog = new()
            {
                PetId = request.PetId,
                CreatedAt = DateTime.UtcNow,
                FoodLevel = pet.FeedNeed
            };
            await _baseRepository.AddAsync(petFeedLog);

            _baseRepository.Update(pet);
            await _baseRepository.SaveChangesAsync();
        }


        public async Task<PetStatResponse> GetPetStats(long petId)
        {
            var bonuses = _petsRepository.GetPetBonuses(petId).ToList();

            var petResponse = await _petsRepository.GetPetStats(petId);

            petResponse = _bonusRepository.GetBonuses(petResponse, bonuses.Select(b => b.Bonus).ToList());

            return petResponse;
        }

        public async Task<List<ArtifactDTO>> GetPetArtifacts(long petId)
        {
            var artifacts = await _artifactRepository.GetPetArtifacts(petId);
            var artifactsDto = ArtifactMapper.ToDTO(artifacts);
            return artifactsDto;
        }

        public async Task<DateTime> Sleep(long petId)
        {
            Pet pet = await _petsRepository.GetPetById(petId);

            int fullSleepSeconds = 5 * 60 * 60;
            int difference = 100 - pet.SleepNeed;
            int secondsToSleep = (fullSleepSeconds * difference) / 100;
            if (secondsToSleep <= 0)
            {
                throw new Exception("Ваш питомец уже полностью отдохнул и не нуждается в дополнительном сне");
            }

            PetSleepLog petSleepLog = new PetSleepLog()
            {
                PetId = petId,
                CreatedAt = DateTime.UtcNow,
                WokeUpAt = DateTime.UtcNow.AddSeconds(secondsToSleep),
                SleepLevelWhenSleep = pet.SleepNeed,
            };

            pet.IsSleeping = true;
            await _baseRepository.AddAsync(petSleepLog);
            await _baseRepository.SaveChangesAsync();
            return petSleepLog.WokeUpAt;
        }
    }
}
