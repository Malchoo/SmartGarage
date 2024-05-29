using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Repostiories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await _context.Services.FindAsync(serviceId);
    }

    public async Task<IEnumerable<Service>> GetServicesByNameAsync(string name)
    {
        return await _context.Services.Where(s => s.Name.Contains(name)).ToListAsync();
    }

    public async Task<Service> AddServiceAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task<Service> UpdateServiceAsync(Service service)
    {
        _context.Entry(service).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task<bool> DeleteServiceAsync(int serviceId)
    {
        var service = await _context.Services.FindAsync(serviceId);
        if (service == null)
            return false;

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<IEnumerable<Service>> GetServicesByServiceOrderIdAsync(int serviceOrderId)
    {
        return await _context.Services
            .Where(s => s.ServiceOrders.Any(so => so.Id == serviceOrderId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Service>> FilterServicesAsync(string searchString, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Services.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
            query = query.Where(s => s.Name.Contains(searchString));

        if (minPrice.HasValue)
            query = query.Where(s => s.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(s => s.Price <= maxPrice.Value);

        return await query.ToListAsync();
    }
}