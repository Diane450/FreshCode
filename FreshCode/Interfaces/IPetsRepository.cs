﻿using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task<Pet> GetPetByUserId(long userId);
        Task<Pet> GetPetById(long id);
        Pet CreatePet(CreatePetRequest request, long userId);
        Task<Level> GelLevelValues(long levelId);
    }
}
