using FreshCode.DbModels;
using FreshCode.Services;
using System.Reflection;

namespace Tests
{
    public class ArtifactDropServiceTest
    {
        [Fact]
        public void CheckValidHistory()
        {
            Banner banner = new Banner()
            {
                Id = 2,
                BannerTypeId = 2,
            };
            List<ArtifactHistory> artifactHistories = new List<ArtifactHistory>();

            IQueryable<BannerItem> query = new List<BannerItem>()
            {
                new BannerItem()
                {
                    BannerId = 2,
                    Artifact = new Artifact
                    {
                        Id = 1,
                        RarityId = 1,
                    },
                },
                new BannerItem()
                {
                    BannerId = 2,
                    Artifact = new Artifact
                    {
                        Id = 2,
                        RarityId = 2,
                    },
                },
                new BannerItem()
                {
                    BannerId = 2,
                    Artifact = new Artifact
                    {
                        Id = 3,
                        RarityId = 3,
                    },
                },
                new BannerItem()
                {
                    BannerId = 1,
                    Artifact = new Artifact
                    {
                        Id = 4,
                        RarityId = 2,
                    },

                },
                new BannerItem()
                {
                    BannerId = 1,
                    Artifact = new Artifact
                    {
                        Id = 4,
                        RarityId = 2,
                    },
                }

            }.AsQueryable();

            var artifactService = new ArtifactDropService();

            for (int i = 0; i < 540; i++)
            {
                var item = artifactService.GetArtifact(banner, artifactHistories, query);
                artifactHistories.Add(new ArtifactHistory
                {
                    Id = 1 + i,
                    UserId = 2,
                    Artifact = new Artifact
                    {
                        Id = item.ArtifactId,
                        RarityId = item.Artifact.RarityId,
                    },
                    BannerId = item.BannerId,
                });
            }
            Assert.True(IsEpicArtifactHistoryValid(artifactHistories, banner));
            Assert.True(IsLegandaryArtifactHistoryValid(artifactHistories, banner));
        }

        private bool IsEpicArtifactHistoryValid(List<ArtifactHistory> artifactHistories, Banner banner)
        {
            var epicArtifacts = artifactHistories
                .Where(ah => ah.Artifact.RarityId == 1).ToList();

            for (int i = 1; i < epicArtifacts.Count; i++)
            {
                if (epicArtifacts[i].BannerId != banner.Id && epicArtifacts[i - 1].BannerId != banner.Id)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsLegandaryArtifactHistoryValid(List<ArtifactHistory> artifactHistories, Banner banner)
        {
            var epicArtifacts = artifactHistories
                .Where(ah => ah.Artifact.RarityId == 2).ToList();

            for (int i = 1; i < epicArtifacts.Count; i++)
            {
                if (epicArtifacts[i].BannerId != banner.Id && epicArtifacts[i - 1].BannerId != banner.Id)
                {
                    return false;
                }
            }
            return true;
        }

    }
}