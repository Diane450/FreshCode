namespace FreshCode.Interfaces
{
    public interface IFortuneRepository
    {
        DateTime? GetUserLastWheelRollTime(long userId);
    }
}
