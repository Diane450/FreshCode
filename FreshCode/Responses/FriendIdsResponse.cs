namespace FreshCode.Responses
{
    public class FriendIdsResponse
    {
        public List<long> Items { get; set; }

        public long Count { get; set; } // Если вам нужно количество друзей
    }
}
