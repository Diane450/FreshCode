namespace FreshCode.ModelsDTO
{
    public class BattleDTO
    {
        public (string ConnectionId, long UserId) Attacker { get; set; }
        public (string ConnectionId, long UserId) Defender { get; set; }
        public long BattleId { get; set; }
    }
}
