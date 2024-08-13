using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class ArtifactMapper
    {
        public static ArtifactDTO ToDTO(Artifact artifact)
        {
            return new ArtifactDTO
            {
                Id = artifact.Id,
                X = artifact.X,
                Y = artifact.Y,
                Price = artifact.Price,
                Rarity = artifact.Rarity.Name,
                Bonus = artifact.ArtifactBonuses.Select(b => BonusMapper.ToDTO(b.Bonus)).ToList(),
                //Characteristic = artifact.Bonus.Characteristic.Characteristic1,
                //Value = artifact.Bonus.Value,
                //Type = artifact.Bonus.Type.Type
            };
        }
    }
}
