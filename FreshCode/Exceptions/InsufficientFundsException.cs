namespace FreshCode.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Недостаточно средств для покупки.") { }
    }
}
