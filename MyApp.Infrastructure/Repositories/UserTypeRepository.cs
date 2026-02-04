using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Infrastructure.Repositories
{
    public class UserTypeRepository: GenericRepository<UserType>,  IUserTypeRepository
    {
        public UserTypeRepository(BankAppDataContext context) : base(context)
        {
        }
    }
}
