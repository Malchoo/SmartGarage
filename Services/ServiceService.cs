using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;
using SmartGarage.Services.Contracts;

namespace SmartGarage.Services;
public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<Service>> GetAllServicesAsync()
    {
        return await _serviceRepository.GetAllServicesAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await _serviceRepository.GetServiceByIdAsync(serviceId);
    }

    public async Task<Service> AddServiceAsync(Service service)
    {
        return await _serviceRepository.AddServiceAsync(service);
    }

    public async Task<Service> UpdateServiceAsync(Service service)
    {
        return await _serviceRepository.UpdateServiceAsync(service);
    }

    public async Task<bool> DeleteServiceAsync(int serviceId)
    {
        return await _serviceRepository.DeleteServiceAsync(serviceId);
    }

    public async Task<IEnumerable<Service>> GetServicesByNameAsync(string name)
    {
        return await _serviceRepository.GetServicesByNameAsync(name);
    }

    public async Task<IEnumerable<Service>> GetServicesByServiceOrderIdAsync(int serviceOrderId)
    {
        return await _serviceRepository.GetServicesByServiceOrderIdAsync(serviceOrderId);
    }

    public async Task<IEnumerable<Service>> FilterServicesAsync(string searchString, decimal? minPrice, decimal? maxPrice)
    {
        return await _serviceRepository.FilterServicesAsync(searchString, minPrice, maxPrice);
    }
}