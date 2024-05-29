using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;
using SmartGarage.Services.Contracts;

namespace SmartGarage.Services;

public class ServiceOrderService : IServiceOrderService
{
    private readonly IServiceOrderRepository _serviceOrderRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly CurrencyApiService _currencyApiService;

    public ServiceOrderService(
        IServiceOrderRepository serviceOrderRepository, 
        IServiceRepository serviceRepository, 
        CurrencyApiService currencyApiService)
    {
        _serviceOrderRepository = serviceOrderRepository;
        _serviceRepository = serviceRepository;
        _currencyApiService = currencyApiService;
    }

    public async Task<IEnumerable<ServiceOrder>> GetAllServiceOrdersAsync()
    {
        return await _serviceOrderRepository.GetAllServiceOrdersAsync();
    }

    public async Task<ServiceOrder?> GetServiceOrderByIdAsync(int serviceOrderId)
    {
        return await _serviceOrderRepository.GetServiceOrderByIdAsync(serviceOrderId);
    }

    public async Task<ServiceOrder> AddServiceOrderAsync(ServiceOrder serviceOrder)
    {
        return await _serviceOrderRepository.AddServiceOrderAsync(serviceOrder);
    }

    public async Task<ServiceOrder> UpdateServiceOrderAsync(ServiceOrder serviceOrder)
    {
        return await _serviceOrderRepository.UpdateServiceOrderAsync(serviceOrder);
    }

    public async Task<bool> DeleteServiceOrderAsync(int serviceOrderId)
    {
        return await _serviceOrderRepository.DeleteServiceOrderAsync(serviceOrderId);
    }

    public async Task<IEnumerable<ServiceOrder>> GetServiceOrdersByCustomerIdAsync(int customerId, int? vehicleId, DateTime? fromDate, DateTime? toDate)
    {
        return await _serviceOrderRepository.GetServiceOrdersByCustomerIdAsync(customerId, vehicleId, fromDate, toDate);
    }

    public async Task<IEnumerable<ServiceOrder>> GetServiceOrdersByVehicleAndCustomerAsync(int vehicleId, int customerId)
    {
        return await _serviceOrderRepository.GetServiceOrdersByVehicleAndCustomerAsync(vehicleId, customerId);
    }

    public async Task<ServiceOrder?> UpdateServiceOrderStatusAsync(int serviceOrderId, ServiceOrderStatus status)
    {
        return await _serviceOrderRepository.UpdateServiceOrderStatusAsync(serviceOrderId, status);
    }

    public async Task<ServiceOrderStatus> GetServiceOrderStatusAsync(int serviceOrderId)
    {
        return await _serviceOrderRepository.GetServiceOrderStatusAsync(serviceOrderId);
    }
    public async Task<decimal> CalculateTotalPriceAsync(int serviceOrderId, string currencyCode)
    {
        var serviceOrder = await _serviceOrderRepository.GetServiceOrderByIdAsync(serviceOrderId);
        if (serviceOrder == null)
        {
            throw new ArgumentException("Invalid service order ID");
        }

        var services = await _serviceRepository.GetServicesByServiceOrderIdAsync(serviceOrderId);
        decimal totalPrice = services.Sum(s => s.Price);

        if (!string.IsNullOrEmpty(currencyCode))
        {
            var exchangeRates = await _currencyApiService.GetExchangeRatesAsync();
            if (exchangeRates.Rates.ContainsKey(currencyCode))
            {
                totalPrice *= exchangeRates.Rates[currencyCode];
            }
        }

        return totalPrice;
    }
}