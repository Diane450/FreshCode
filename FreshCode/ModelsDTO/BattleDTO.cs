using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    public class BattleDTO
    {
        public (string ConnectionId, long UserId, PetDTO pet) Attacker { get; set; }
        public (string ConnectionId, long UserId, PetDTO pet) Defender { get; set; }
        public long BattleId { get; set; }
    }
}
