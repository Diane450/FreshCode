using System.Data;

namespace FreshCode
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection Create();
    }
}
