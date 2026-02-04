using MyApp.Application.Dtos;
using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserType GetUserTypeById(int id)
        {
            var userType = _unitOfWork.UserTypeRepository.GetById(id);
            return userType; 
        }

    }
}
