
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IUserRepository
    {
        Task <UserDTO> GetUserGameInfo(string vk_user_id);
        Task InventoryDecreaseFoodCount(string vk_user_id, FoodDTO food);
    }
}
