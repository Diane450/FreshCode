using FreshCode.DbModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace FreshCode.EF_Repositories
{
    public class TransactionRepository(FreshCodeContext dbContext)
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IDbTransaction BeginTransaction()
        {
            var transaction = _dbContext.Database.BeginTransaction();

            return transaction.GetDbTransaction();
        }
    }
}
