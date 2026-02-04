using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface ITransactionsRepository : IGenericRepository<Transaction>
    {
        IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);
    }
}
