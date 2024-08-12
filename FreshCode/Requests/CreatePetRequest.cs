namespace FreshCode.Requests
{
    public class CreatePetRequest
    {
        public string Name { get; set; } = null!;
        public int Body_Id { get; set; }
        public int Eyes_Id { get; set; }
    }
}
