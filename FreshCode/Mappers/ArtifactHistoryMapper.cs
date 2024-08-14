using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using System.Threading.Tasks;

namespace FreshCode.Mappers
{
    public static class ArtifactHistoryMapper
    {
        public static ArtifactHistoryDTO ToDTO(ArtifactHistory artifactHistory)
        {
            return new ArtifactHistoryDTO
            {
                Id = artifactHistory.Id,
                Artifact = ArtifactMapper.ToDTO(artifactHistory.Artifact),
                GotAt = artifactHistory.GotAt
            };
        }
    }
}
