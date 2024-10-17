namespace FreshCode.ModelsDTO
{
    public class BattlerDTO
    {
        public string ConnectionId { get; set; } = null!;
        public long vk_user_id { get; set; }
        public long UserId { get; set; }
        public PetDTO Pet { get; set; } = null!;
        public int MoveCount { get; set; }
    }
}
