using FreshCode.ModelsDTO;

namespace FreshCode.Responses
{
    public class AttackResponse
    {
        public (string ConnectionId, long UserId, PetBattleDTO pet, long vk_user_id, int Movecount) Winner { get; set; }
        public (string ConnectionId, long UserId, PetBattleDTO pet, long vk_user_id, int Movecount) Loser { get; set; }
        public RewardResponse Reward { get; set; } = null!;
    }
}
