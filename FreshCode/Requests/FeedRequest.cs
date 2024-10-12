using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace FreshCode.Requests
{
    public class FeedRequest
    {
        public long PetId { get; set; }
        public long FoodId{ get; set; }
    }
}
