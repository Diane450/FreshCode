namespace FreshCode.Interfaces
{
    public interface IMiddleWare
    {
        public Dictionary<string, string> QueryParams { get; protected set; }

        public bool VerifySignature(IHeaderDictionary header);

        public Task<long> GetInnerId();

        public (bool, int) Verify(HttpContext httpContext);
    }
}
