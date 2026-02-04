using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Loan GetLoanByAccountId(int accountId);
    }
}