namespace FreshCode.ModelsDTO
{
    /// <summary>
    /// DTO еда ользователя 
    /// </summary>
    public class UserFoodDTO
    {
        /// <summary>
        /// Id записи в таблице
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// DTO еда
        /// </summary>
        public FoodDTO Food { get; set; } = null!;
        /// <summary>
        /// кол-во
        /// </summary>
        public int Count { get; set; }
    }
}
