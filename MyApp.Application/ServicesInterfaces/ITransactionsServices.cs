using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;
using Transaction = MyApp.Domain.Entities.Transaction;

namespace MyApp.Application.ServicesInterfaces
{
    public interface ITransactionsServices
    {
        IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);

        Transaction AddTransaction(Transaction transaction);

        string TransactionBetweenTwoAccounts(int accountIdSender, int accountIdReceiver, int amount, Account accountSender, decimal balanceSender);
    }
}
