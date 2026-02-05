using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Application.ServicesInterfaces;
using MyApp.Domain.Entities;
using Transaction = MyApp.Domain.Entities.Transaction;

namespace MyApp.Application.Services
{
    public class TransactionsServices : ITransactionsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            _unitOfWork.TransactionsRepository.Add(transaction);
            _unitOfWork.Complete();
            return transaction;
        }

        public IEnumerable<Transaction> GetTransactionsByAccountId(int accountId)
        {
            var transactions = _unitOfWork.TransactionsRepository.GetTransactionsByAccountId(accountId);
            return transactions;
        }

        public string TransactionBetweenTwoAccounts(int accountIdSender, int accountIdReceiver, int amount, Account accountSender, decimal balanceSender, IAccountService accountService)
        {
            if (amount > balanceSender)
            {
                return ("You do not have enough money to transact.");
            }
            else
            {
                var transactionSender = new Transaction
                {
                    AccountId = accountIdSender,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Type = "Credit",
                    Operation = "Credit in cash",
                    Amount = amount,
                    Balance = balanceSender
                };

                AddTransaction(transactionSender);
                accountSender.Balance -= amount;
                accountService.UpdateAccount(accountSender);

                var accountReceiver = accountService.GetAccount(accountIdReceiver);
                var transactionReciever = new Transaction
                {
                    AccountId = accountIdReceiver,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Type = "Credit",
                    Operation = "Credit in cash",
                    Amount = amount,
                    Balance = accountReceiver.Balance
                };

                AddTransaction(transactionReciever);
                accountReceiver.Balance += amount;
                accountService.UpdateAccount(accountReceiver);
                return ("The transaction is complete.");
            }
        }
    }
}
