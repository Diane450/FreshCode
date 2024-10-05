using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Services
{
    public class ArtifactService(IPetBonusManagerService petBonusManagerService) : IArtifactService
    {
        private readonly IPetBonusManagerService _petBonusManagerService = petBonusManagerService;

        //TODO: обновить среднюю силу питомца
        public void AssignArtifact(Pet pet, Artifact artifact)
        {
            switch (artifact.ArtifactType.Type)
            {
                case "Шапка":
                    pet.HatId = artifact.Id;
                    EquipArtifact(pet, artifact, pet.Hat);
                    break;

                case "Аксессуар":
                    pet.AccessoryId = artifact.Id;
                    EquipArtifact(pet, artifact, pet.Accessory);
                    break;
            }
        }

        private void EquipArtifact(Pet pet, Artifact newArtifact, Artifact? currentArtifact)
        {
            if (currentArtifact is not null)
            {
                _petBonusManagerService.RemoveBonuses(pet, currentArtifact.ArtifactBonuses.Select(ab => ab.Bonus).ToList());
            }
            _petBonusManagerService.SetBonuses(pet, newArtifact.ArtifactBonuses.Select(ab => ab.Bonus).ToList());
        }

        public void RemoveArtifact(Pet pet, Artifact artifact)
        {
            switch (artifact.ArtifactType.Type)
            {
                case "Шапка":
                    _petBonusManagerService.RemoveBonuses(pet, pet.Hat.ArtifactBonuses.Select(ab => ab.Bonus).ToList());
                    pet.Hat = null;
                    break;

                case "Аксессуар":
                    _petBonusManagerService.RemoveBonuses(pet, pet.Accessory.ArtifactBonuses.Select(ab => ab.Bonus).ToList());
                    pet.Accessory = null;
                    break;
            }
        }
    }
}
