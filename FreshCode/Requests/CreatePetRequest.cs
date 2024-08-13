using FreshCode.DbModels;

namespace FreshCode.Requests
{
    public class CreatePetRequest
    {
        public string Name { get; set; } = null!;
        public Body Body { get; set; } = null!;
        public Eye Eyes { get; set; } = null!;
    }
}
