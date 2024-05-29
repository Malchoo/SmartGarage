using SmartGarage.Models;

namespace SmartGarage.Repostiories.Contracts;

public interface IServiceRepository
{
    Task<Service?> GetServiceByIdAsync(int serviceId);
    Task<IEnumerable<Service>> GetServicesByNameAsync(string name);
    Task<Service> AddServiceAsync(Service service);
    Task<Service> UpdateServiceAsync(Service service);
    Task<bool> DeleteServiceAsync(int serviceId);
    Task<IEnumerable<Service>> GetAllServicesAsync();
    Task<IEnumerable<Service>> GetServicesByServiceOrderIdAsync(int serviceOrderId);
    Task<IEnumerable<Service>> FilterServicesAsync(string searchString, decimal? minPrice, decimal? maxPrice);
}