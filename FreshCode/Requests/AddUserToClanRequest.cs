namespace FreshCode.Requests
{
    public class AddUserToClanRequest
    {
        public long UserIdToAdd { get; set; }
        public long ClanId { get; set; }
        public long RoleId { get; set; }
    }
}
