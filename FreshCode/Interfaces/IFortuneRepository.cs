namespace FreshCode.Interfaces
{
    public interface IFortuneRepository
    {
        Task<DateTime> GetUserLastWheelRollTime(long userId);
    }
}
