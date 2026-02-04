using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateLoan(Loan loan)
        {
            _unitOfWork.LoanRepository.Update(loan);
            _unitOfWork.Complete();
        }
    }
}
