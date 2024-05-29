using SmartGarage.Models;

namespace SmartGarage.Services.Contracts;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAllServicesAsync();
    Task<Service?> GetServiceByIdAsync(int serviceId);
    Task<Service> AddServiceAsync(Service service);
    Task<Service> UpdateServiceAsync(Service service);
    Task<bool> DeleteServiceAsync(int serviceId);
    Task<IEnumerable<Service>> GetServicesByNameAsync(string name);
    Task<IEnumerable<Service>> GetServicesByServiceOrderIdAsync(int serviceOrderId);
    Task<IEnumerable<Service>> FilterServicesAsync(string searchString, decimal? minPrice, decimal? maxPrice);
}