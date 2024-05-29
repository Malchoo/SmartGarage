using SmartGarage.Models;

namespace SmartGarage.Services.Contracts;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int customerId);
    Task<IEnumerable<Customer>> FilterCustomersAsync(string name, string email, string phone, string vehicleModel, string vehicleMake, DateTime? fromDate, DateTime? toDate);
}