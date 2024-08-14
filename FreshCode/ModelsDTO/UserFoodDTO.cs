namespace FreshCode.ModelsDTO
{
    public class UserFoodDTO
    {
        public long Id { get; set; }
        public FoodDTO Food { get; set; } = null!;
        public int Count { get; set; }
    }
}
