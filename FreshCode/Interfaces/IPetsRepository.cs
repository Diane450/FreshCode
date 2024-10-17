using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Responses;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task<Pet> GetPetByUserId(long userId);
        Task<Pet> GetPetById(long id);
        Pet CreatePet(CreatePetRequest request, long userId);
        Task<Level> GelLevelValues(long levelId);
        IQueryable<UserBonuse> GetPetBonuses(long petId);
        Task<PetStatResponse> GetPetStats(long petId);
        IQueryable<PetFeedLog> GetFeedPetLogLast5Minute(long petId);
        Task<bool> IsPetSleeping(long vkUserId);
    }
}
