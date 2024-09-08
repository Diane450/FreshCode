namespace FreshCode.ModelsDTO
{
    public class FoodDTO
    {
        public long FoodId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public string Name { get; set; } = null!;

        public int Price { get; set; }

        public List<BonusDTO> Bonuses { get; set; } = null!;
    }
}
