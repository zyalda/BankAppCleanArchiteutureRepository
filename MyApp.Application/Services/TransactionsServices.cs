using MyApp.Application.Interfaces;
using MyApp.Application.ServicesInterfaces;
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
    }
}
