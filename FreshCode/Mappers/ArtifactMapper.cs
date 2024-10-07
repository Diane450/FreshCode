using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using System.Net;

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
                Rarity = artifact.Rarity.Rarity1,
                Type = artifact.ArtifactType.Type,
                Bonuses = artifact.ArtifactBonuses.Select(b => BonusMapper.ToDTO(b.Bonus)).ToList(),
            };
        }

        public static List<ArtifactDTO> ToDTO(List<Artifact> artifacts)
        {
            var list = new List<ArtifactDTO>();
            foreach (var artifact in artifacts)
            {
                list.Add(ToDTO(artifact));
            }
            return list;
        }
    }
}
