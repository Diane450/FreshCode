using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class EatRequest
    {
        public int PetId { get; set; }
        public FoodDTO Food { get; set; } = null!;
    }
}
