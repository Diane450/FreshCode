using FreshCode.DbModels;

namespace FreshCode.ModelsDTO
{
    public class BattleDTO
    {
        public (string ConnectionId, long UserId, PetDTO pet, long vk_user_id, int Movecount) Attacker { get; set; }
        public (string ConnectionId, long UserId, PetDTO pet, long vk_user_id, int Movecount) Defender { get; set; }
        public long BattleId { get; set; }
    }
}
