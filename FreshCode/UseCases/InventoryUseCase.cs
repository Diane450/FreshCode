
using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class InventoryUseCase(IUserRepository userRepository, IPetsRepository petsRepository)
    {
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async System.Threading.Tasks.Task SetBackground(long backgroundId, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);
            user.BackgroundId = backgroundId;
            await _userRepository.SaveChangesAsync();
        }

        //public async System.Threading.Tasks.Task SetArtifact(SetArtifactRequest setArtifactRequest)
        //{
        //    Pet pet = await _petsRepository.GetPetById(setArtifactRequest.Pet.Id);

        //    PetDTO petDTO = setArtifactRequest.Pet;
        //    ArtifactDTO newArtifact = setArtifactRequest.Artifact;

        //    if (newArtifact.Type == "Шапка")
        //    {

        //        pet.HatId = setArtifactRequest.Artifact.Id;
        //    }
        //    else
        //    {
        //        pet.AccessoryId = setArtifactRequest.Artifact.Id;
        //    }
        //    await _petsRepository.SaveShangesAsync();
        //}
    }
}
