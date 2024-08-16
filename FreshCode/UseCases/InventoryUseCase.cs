
using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class InventoryUseCase(IUserRepository userRepository)
    {
        //private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async System.Threading.Tasks.Task SetBackground(BackgroundDTO backgroundDTO, string vk_user_id)
        {
            User user = await _userRepository.GetUserIdByVkId(vk_user_id);
            user.BackgroundId = backgroundDTO.Id;
            await _userRepository.SaveChangesAsync();
        }
    }
}
