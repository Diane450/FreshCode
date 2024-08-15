using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class CreatePetRequest
    {
        public string Name { get; set; } = null!;
        public BodyDTO Body { get; set; } = null!;
        public EyeDTO Eyes { get; set; } = null!;
    }
}
