using FreshCode.DbModels;
using System;
using System.Runtime.CompilerServices;

namespace FreshCode.Services
{
    public class ArtifactDropService
    {
        private int Rarity { get; set; }

        private int CountSinceLastRare5 { get; set; }

        private int CountSinceLastRare4 { get; set; }

        private bool Last5RareWasBanner { get; set; }

        private bool Last4RareWasBanner { get; set; }

        private BannerItem GetArtifactEventBanner(Random random, IQueryable<BannerItem> bannerItems, Banner banner)
        {
            BannerItem selectedItem;
            if (Rarity == 5)
            {
                if (!Last5RareWasBanner || random.Next(0, 2) == 1)
                {
                    //выбираем рандом артефакт 5* из баннерного списка артефактов
                    var filtereditems = bannerItems.Where(b => b.BannerId == banner.Id);
                    var items = filtereditems.Where(bi => bi.Artifact.RarityId == 2).ToList();
                    selectedItem = items[random.Next(items.Count())];
                }
                else
                {
                    //выбираем рандом артефакт 5* из общего списка артефактов
                    var items = bannerItems.Where(bi => bi.Artifact.RarityId == 2).ToList();
                    selectedItem = items[random.Next(items.Count())];
                }
            }
            else if (Rarity == 4)
            {
                if (!Last4RareWasBanner || random.Next(0, 2) == 1)
                {
                    bannerItems = bannerItems.Where(b => b.BannerId == banner.Id);
                    var items = bannerItems.Where(bi => bi.Artifact.RarityId == 1).ToList();
                    selectedItem = items[random.Next(items.Count())];
                }
                else
                {
                    var items = bannerItems.Where(bi => bi.Artifact.RarityId == 1).ToList();
                    selectedItem = items[random.Next(items.Count())];
                }
            }

            else
            {
                var items = bannerItems.Where(bi => bi.Artifact.RarityId == 3).ToList();
                selectedItem = items[random.Next(items.Count())];
            }
            return selectedItem;
        }

        public BannerItem GetArtifact(Banner banner, List<ArtifactHistory> history, IQueryable<BannerItem> artifacts)
        {
            Random random = new Random();
            history = history.OrderByDescending(h => h.Id).ToList();

            int lastRare5Index = GetLastRare5Index(history);
            int lastRare4Index = GetLastRare4Index(history);

            CountSinceLastRare4 = lastRare4Index != -1 ? lastRare4Index : history.Count;
            CountSinceLastRare5 = lastRare5Index != -1 ? lastRare5Index : history.Count;

            int roll = random.Next(1, 1001);
            Rarity = GetRarity(roll);

            Last5RareWasBanner = lastRare5Index != -1 && history[lastRare5Index].BannerId == banner.Id;
            Last4RareWasBanner = lastRare4Index != -1 && history[lastRare4Index].BannerId == banner.Id;

            if (banner.BannerTypeId == 1)
                return GetArtifactStandartBanner(random, artifacts, banner);
            return GetArtifactEventBanner(random, artifacts, banner);
        }
        private BannerItem GetArtifactStandartBanner(Random random, IQueryable<BannerItem> bannerItems, Banner banner)
        {
            bannerItems = bannerItems.Where(b => b.BannerId == banner.Id);
            BannerItem selectedItem;
            if (Rarity == 5)
            {
                var items = bannerItems.Where(bi => bi.Artifact.RarityId == 2).ToList();
                selectedItem = items[random.Next(items.Count())];
            }
            else if (Rarity == 4)
            {
                var items = bannerItems.Where(bi => bi.Artifact.RarityId == 1).ToList();
                selectedItem = items[random.Next(items.Count())];
            }
            else
            {
                var items = bannerItems.Where(bi => bi.Artifact.RarityId == 3).ToList();
                selectedItem = items[random.Next(items.Count())];
            }
            return selectedItem;
        }

        private int GetRarity(int roll)
        {
            if (CountSinceLastRare5 >= 89 || roll >= 994)
                return 5;
            else if (CountSinceLastRare4 >= 9 || roll >= 943)
                return 4;
            return 3;
        }

        private int GetLastRare5Index(List<ArtifactHistory> history)
        {
            return history.FindIndex(h => h.Artifact.RarityId == 2);
        }

        private int GetLastRare4Index(List<ArtifactHistory> history)
        {
            return history.FindIndex(h => h.Artifact.RarityId == 1);
        }
    }
}
