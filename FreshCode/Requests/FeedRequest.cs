using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class FeedRequest
    {
        public PetDTO Pet { get; set; } = null!;
        public FoodDTO Food { get; set; } = null!;
    }
}
