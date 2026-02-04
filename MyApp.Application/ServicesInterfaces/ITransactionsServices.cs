using Transaction = MyApp.Domain.Entities.Transaction;

namespace MyApp.Application.ServicesInterfaces
{
    public interface ITransactionsServices
    {
        IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);

        Transaction AddTransaction(Transaction transaction);
    }
}
