using MyApp.Domain.Entities;

namespace MyApp.Application.ServiceInterfaces
{
    public interface ILoanService
    {
        public void CreateLoan(Loan loan);
    }
}
