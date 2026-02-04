using MyApp.Application.Dtos;
using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.StaticUserRoles;

namespace MyApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Customer GetCustomerById(int id)
        {
            var customer = _unitOfWork.CustomerRepository.GetById(id);
            return customer; 
        }

        public Customer AddNewCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Add(customer);
            var result = _unitOfWork.Complete();
            return customer;
        }

        public string AddNewCustomer(string userType, string gender, string givenname, string surname,
                                            string streetaddress, string city, string zipcode, string country, string countryCode, string telephonecountrycode, string telephonenumber, string emailaddress)
        {
            if (userType.ToLower() == StaticUserRoles.Customer.ToLower())
            {
                var customer = new Customer
                {
                    Gender = gender,
                    Givenname = givenname,
                    Surname = surname,
                    Streetaddress = streetaddress,
                    City = city,
                    Zipcode = zipcode,
                    Country = country,
                    CountryCode = countryCode,
                    Emailaddress = emailaddress,
                    UserTypeId = 2
                };

                var newCustomer = AddNewCustomer(customer);

                return ($"The Customer {newCustomer.CustomerId} has been added.");
            }
            else if (userType == StaticUserRoles.Admin.ToLower())
            {
                return ($"{StaticUserRoles.Admin} not allowed to add admin user. Only customer type.");
            }
            else { return ($"Add {StaticUserRoles.Admin} or {StaticUserRoles.Customer} as user type."); }
        }
    }
}
