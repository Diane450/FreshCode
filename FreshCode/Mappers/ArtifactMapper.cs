using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class ArtifactMapper
    {
        public static ArtifactDTO ToDTO(Artifact? artifact)
        {
            if (artifact is null)
            {
                return null;
            }
            return new ArtifactDTO
            {
                Id = artifact.Id,
                X = artifact.X,
                Y = artifact.Y,
                Price = artifact.Price,
                Rarity = artifact.Rarity.Name,
                Type = artifact.ArtifatcType.Type,
                Bonuses = artifact.ArtifactBonuses.Select(b => BonusMapper.ToDTO(b.Bonus)).ToList(),
            };
        }

        public static ArtifactSummaryDTO? ToArtifactSummaryDTO(Artifact? artifact)
        {
            if (artifact is null)
            {
                return null;
            }
            return new ArtifactSummaryDTO
            {
                Id = artifact.Id,
                X = artifact.X,
                Y = artifact.Y,
            };
        }
    }
}
