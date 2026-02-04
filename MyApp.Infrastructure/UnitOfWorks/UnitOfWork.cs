using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public UnitOfWork(BankAppDataContext context)
        {
            _bankAppDataContext = context;
            CustomerRepository = new CustomerRepository(_bankAppDataContext);
            CustomerAccountRepository = new CustomerAccountRepository(_bankAppDataContext);
            AccountRepository = new AccountRepository(_bankAppDataContext);
            AccountTypeRepository = new AccountTypeRepository(_bankAppDataContext);
            DispositionRepository = new DispositionRepository(_bankAppDataContext);
            LoanRepository = new LoanRepository(_bankAppDataContext);
            TransactionsRepository = new TransactionsRepository(_bankAppDataContext);
            UserTypeRepository = new UserTypeRepository(_bankAppDataContext);

        }

        public IAccountRepository AccountRepository { get; private set; }
        public IAccountTypeRepository AccountTypeRepository { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public ICustomerAccountRepository CustomerAccountRepository { get; private set; }
        public IDispositionRepository DispositionRepository { get; private set; }
        public ILoanRepository LoanRepository { get; private set; }
        public ITransactionsRepository TransactionsRepository { get; private set; }
        public IUserTypeRepository UserTypeRepository { get; private set; }

        public int Complete()
        {
            return _bankAppDataContext.SaveChanges();
        }

        public void Dispose()
        {
            _bankAppDataContext.Dispose();
        }
    }
}
