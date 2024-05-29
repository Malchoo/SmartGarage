using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;
using SmartGarage.Services.Contracts;

namespace SmartGarage.Services;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _customerRepository.GetAllCustomersAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _customerRepository.GetCustomerByIdAsync(customerId);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        return await _customerRepository.AddCustomerAsync(customer);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        return await _customerRepository.UpdateCustomerAsync(customer);
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        return await _customerRepository.DeleteCustomerAsync(customerId);
    }

    public async Task<IEnumerable<Customer>> FilterCustomersAsync(string name, string email, string phone, string vehicleModel, string vehicleMake, DateTime? fromDate, DateTime? toDate)
    {
        return await _customerRepository.FilterCustomersAsync(name, email, phone, vehicleModel, vehicleMake, fromDate, toDate);
    }
}