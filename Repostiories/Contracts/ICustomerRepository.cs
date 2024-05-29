using SmartGarage.Models;

namespace SmartGarage.Repostiories.Contracts;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int customerId);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<IEnumerable<Customer>> FilterCustomersAsync(string name, string email, string phone, string vehicleModel, string vehicleMake, DateTime? fromDate, DateTime? toDate);
}