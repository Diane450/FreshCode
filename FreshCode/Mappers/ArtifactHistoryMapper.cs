using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using System.Threading.Tasks;

namespace FreshCode.Mappers
{
    public static class ArtifactHistoryMapper
    {
        public static ArtifactHistoryDTO ToDTO(Artifact artifact)
        {
            var userHistory = artifact.ArtifactHistories.FirstOrDefault();
            
            return new ArtifactHistoryDTO
            {
                Id = artifact.Id,
                Artifact = ArtifactMapper.ToDTO(artifact),
                GotAt = userHistory.GotAt
            };
        }
    }
}
