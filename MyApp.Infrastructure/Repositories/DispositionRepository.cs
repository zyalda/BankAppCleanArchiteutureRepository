using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Repositories
{
    public class DispositionRepository : GenericRepository<Disposition>, IDispositionRepository
    {
        public DispositionRepository(BankAppDataContext context) : base(context)
        {
        }
    }
}
