using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Repositories
{
    public class TransactionsRepository : GenericRepository<Transaction>, ITransactionsRepository
    {
        public TransactionsRepository(BankAppDataContext context) : base(context)
        {
        }

        public IEnumerable<Transaction> GetTransactionsByAccountId(int accountId)
        {
            var transactions = _context.Transactions.Where(x => x.AccountId == accountId);
            return transactions;
        }
    }
}
