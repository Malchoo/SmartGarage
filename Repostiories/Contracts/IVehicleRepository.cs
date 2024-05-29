using SmartGarage.Models;

namespace SmartGarage.Repostiories.Contracts;

public interface IVehicleRepository
{
    Task<Vehicle?> GetVehicleByIdAsync(int vehicleId);
    Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
    Task<bool> DeleteVehicleAsync(int vehicleId);
    Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
    Task<IEnumerable<Vehicle>> GetVehiclesByLicensePlateAsync(string licensePlate);
    Task<IEnumerable<Vehicle>> GetVehiclesByVinAsync(string vin);
    Task<IEnumerable<Vehicle>> GetVehiclesByOwnerIdAsync(int ownerId);
    Task<IEnumerable<Vehicle>> FilterVehiclesAsync(string model, string brand, int year);
    Task<VehicleBrand?> GetVehicleBrandByNameAsync(string name);
    Task<VehicleBrand> AddVehicleBrandAsync(VehicleBrand brand);
    Task<VehicleModel?> GetVehicleModelByNameAsync(string name);
    Task<VehicleModel> AddVehicleModelAsync(VehicleModel model);
}