using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(BankAppDataContext context) : base(context)
        {
        }
        
        public  Loan GetLoanByAccountId(int accountId)
        {
            var loan =  _context.Loans.Where(x => x.AccountId == accountId).SingleOrDefault();
            if(loan == null)
                return new Loan();
            return loan;
        }
    }
}
