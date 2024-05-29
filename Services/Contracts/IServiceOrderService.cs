using SmartGarage.Models;

namespace SmartGarage.Services.Contracts;

public interface IServiceOrderService
{
    Task<IEnumerable<ServiceOrder>> GetAllServiceOrdersAsync();
    Task<ServiceOrder?> GetServiceOrderByIdAsync(int serviceOrderId);
    Task<ServiceOrder> AddServiceOrderAsync(ServiceOrder serviceOrder);
    Task<ServiceOrder> UpdateServiceOrderAsync(ServiceOrder serviceOrder);
    Task<bool> DeleteServiceOrderAsync(int serviceOrderId);
    Task<IEnumerable<ServiceOrder>> GetServiceOrdersByCustomerIdAsync(int customerId, int? vehicleId, DateTime? fromDate, DateTime? toDate);
    Task<IEnumerable<ServiceOrder>> GetServiceOrdersByVehicleAndCustomerAsync(int vehicleId, int customerId);
    Task<ServiceOrder?> UpdateServiceOrderStatusAsync(int serviceOrderId, ServiceOrderStatus status);
    Task<ServiceOrderStatus> GetServiceOrderStatusAsync(int serviceOrderId);
    Task<decimal> CalculateTotalPriceAsync(int serviceOrderId, string currencyCode);
}