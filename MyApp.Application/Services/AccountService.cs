using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Account CreateNewAccount(string frequency, int balance, int accountTypesId)
        {
            return new Account
            {
                AccountTypesId = accountTypesId,
                Frequency = frequency,
                Balance = balance,
                Created = DateOnly.FromDateTime(DateTime.Now),
            };
        }
        public Account AddAccount(string frequency, int balance, int accountTypesId)
        {
            var account = CreateNewAccount(frequency, balance, accountTypesId);
            _unitOfWork.AccountRepository.Add(account);
            _unitOfWork.Complete();
            return account;
        }

        public Account GetAccount(int id)
        {
            return _unitOfWork.AccountRepository.GetById(id);
        }

        public void UpdateAccount(Account account)
        {
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.Complete();
        }
    }
}
