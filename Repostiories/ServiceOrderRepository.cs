using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Repostiories;

public class ServiceOrderRepository : IServiceOrderRepository
{
    private readonly AppDbContext _context;

    public ServiceOrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceOrder?> GetServiceOrderByIdAsync(int serviceOrderId)
    {
        return await _context.ServiceOrders.FindAsync(serviceOrderId);
    }

    public async Task<ServiceOrder> AddServiceOrderAsync(ServiceOrder serviceOrder)
    {
        _context.ServiceOrders.Add(serviceOrder);
        await _context.SaveChangesAsync();
        return serviceOrder;
    }

    public async Task<ServiceOrder> UpdateServiceOrderAsync(ServiceOrder serviceOrder)
    {
        _context.Entry(serviceOrder).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return serviceOrder;
    }

    public async Task<bool> DeleteServiceOrderAsync(int serviceOrderId)
    {
        var serviceOrder = await _context.ServiceOrders.FindAsync(serviceOrderId);
        if (serviceOrder == null)
            return false;

        serviceOrder.IsDeleted = true;
        _context.Update(serviceOrder);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ServiceOrder>> GetAllServiceOrdersAsync()
    {
        return await _context.ServiceOrders.ToListAsync();
    }

    public async Task<IEnumerable<ServiceOrder>> GetServiceOrdersByCustomerIdAsync(int customerId, int? vehicleId, DateTime? fromDate, DateTime? toDate)
    {
        var query = _context.ServiceOrders.Where(so => so.CustomerId == customerId);

        if (vehicleId.HasValue)
        {
            query = query.Where(so => so.VehicleId == vehicleId.Value);
        }

        if (fromDate.HasValue)
        {
            query = query.Where(so => so.Date >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(so => so.Date <= toDate.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<ServiceOrder>> GetServiceOrdersByVehicleAndCustomerAsync(int vehicleId, int customerId)
    {
        return await _context.ServiceOrders
            .Where(so => so.VehicleId == vehicleId && so.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<ServiceOrder?> UpdateServiceOrderStatusAsync(int serviceOrderId, ServiceOrderStatus status)
    {
        var serviceOrder = await _context.ServiceOrders.FindAsync(serviceOrderId);
        if (serviceOrder == null)
            return null;

        serviceOrder.Status = status;
        await _context.SaveChangesAsync();
        return serviceOrder;
    }

    public async Task<ServiceOrderStatus> GetServiceOrderStatusAsync(int serviceOrderId)
    {
        var serviceOrder = await _context.ServiceOrders.FindAsync(serviceOrderId);
        return serviceOrder?.Status ?? ServiceOrderStatus.NotStarted;
    }

    public Task<IEnumerable<ServiceOrder>> GetCustomerByIdAsync(int customerId)
    {
        throw new NotImplementedException();
    }
}