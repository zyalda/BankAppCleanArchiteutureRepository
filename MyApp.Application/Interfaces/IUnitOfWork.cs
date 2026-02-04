

namespace MyApp.Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        ICustomerAccountRepository CustomerAccountRepository { get; }
        IAccountRepository AccountRepository { get; }
        IUserTypeRepository UserTypeRepository { get; }
        ITransactionsRepository TransactionsRepository { get; }
        ILoanRepository LoanRepository { get; }
        int Complete();
    }
}
