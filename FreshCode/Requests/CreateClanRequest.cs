namespace FreshCode.Requests
{
    public class CreateClanRequest
    {
        public long CreatorId { get; set; }
        public string ClanName { get; set; } = null!;
    }
}
