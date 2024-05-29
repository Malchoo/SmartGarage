using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Repostiories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer == null)
            return false;

        customer.IsDeleted = true;
        _context.Update(customer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<IEnumerable<Customer>> FilterCustomersAsync(string name, string email, string phone, string vehicleModel, string vehicleMake, DateTime? fromDate, DateTime? toDate)
    {
        var query = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(c => c.User.Username.Contains(name));

        if (!string.IsNullOrWhiteSpace(email))
            query = query.Where(c => c.User.Email.Contains(email));

        if (!string.IsNullOrWhiteSpace(phone))
            query = query.Where(c => c.User.PhoneNumber.Contains(phone));

        if (!string.IsNullOrWhiteSpace(vehicleModel))
            query = query.Where(c => c.Vehicles.Any(v => v.Model.Name.Contains(vehicleModel)));

        if (!string.IsNullOrWhiteSpace(vehicleMake))
            query = query.Where(c => c.Vehicles.Any(v => v.Brand.Name.Contains(vehicleMake)));

        if (fromDate.HasValue)
            query = query.Where(c => c.ServiceOrders.Any(so => so.Date >= fromDate));

        if (toDate.HasValue)
            query = query.Where(c => c.ServiceOrders.Any(so => so.Date <= toDate));

        return await query.ToListAsync();
    }

}