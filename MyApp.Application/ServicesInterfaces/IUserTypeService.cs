using MyApp.Domain.Entities;

namespace MyApp.Application.ServiceInterfaces
{
    public interface IUserTypeService
    {
        public UserType GetUserTypeById(int id);
    }
}
