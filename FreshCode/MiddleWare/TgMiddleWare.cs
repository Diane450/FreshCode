using FreshCode.Interfaces;

namespace FreshCode.MiddleWare
{
    public class TgMiddleWare : IMiddleWare
    {
        Dictionary<string, string> IMiddleWare.QueryParams { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public long GetInnerId(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public (bool, int) Verify(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public bool VerifySignature(IHeaderDictionary header)
        {
            throw new NotImplementedException();
        }

        Task<long> IMiddleWare.GetInnerId()
        {
            throw new NotImplementedException();
        }

        (bool, int) IMiddleWare.Verify(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        bool IMiddleWare.VerifySignature(IHeaderDictionary header)
        {
            throw new NotImplementedException();
        }
    }
}
