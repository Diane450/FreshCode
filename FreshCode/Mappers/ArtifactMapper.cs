using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class ArtifactMapper
    {
        public static ArtifactDTO ToArtifactDTO(ArtifactBonuse artifact)
        {
            return new ArtifactDTO
            {
                Id = artifact.Id,
                X = artifact.Artifact.X,
                Y = artifact.Artifact.Y,
                Price = artifact.Artifact.Price,
                Rarity = artifact.Artifact.Rarity.Name,
                Bonus = $"{artifact.Bonus.Characteristic.Characteristic1} +{artifact.Bonus.Value}{(artifact.Bonus.Type.Type == "percent" ? '%' : String.Empty)} ",
                Characteristic = artifact.Bonus.Characteristic.Characteristic1,
                Value = artifact.Bonus.Value,
                Type = artifact.Bonus.Type.Type
            };
        }

        public static ArtifactSummaryDTO? ToArtifactSummaryDTO(Artifact? artifact)
        {
            if (artifact is not null)
            {
                return new ArtifactSummaryDTO
                {
                    Id = artifact.Id,
                    X = artifact.X,
                    Y = artifact.Y,
                };
            }
            return null;
        }
    }
}
