using MyApp.Domain.Entities;

namespace MyApp.Application.ServiceInterfaces
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        string AddNewCustomer(string userType, string gender, string givenname, string surname,
                                            string streetaddress, string city, string zipcode, string country, string countryCode, string telephonecountrycode, string telephonenumber, string emailaddress);

    }
}
