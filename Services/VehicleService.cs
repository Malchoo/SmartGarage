using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _vehicleRepository.GetAllVehiclesAsync();
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
    {
        return await _vehicleRepository.GetVehicleByIdAsync(vehicleId);
    }

    public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
    {
        var model = await _vehicleRepository.GetVehicleModelByNameAsync(vehicle.Model.Name);
        if (model == null)
        {
            model = new VehicleModel { Name = vehicle.Model.Name };
            await _vehicleRepository.AddVehicleModelAsync(model);
        }

        var brand = await _vehicleRepository.GetVehicleBrandByNameAsync(vehicle.Brand.Name);
        if (brand == null)
        {
            brand = new VehicleBrand { Name = vehicle.Brand.Name };
            await _vehicleRepository.AddVehicleBrandAsync(brand);
        }

        vehicle.ModelId = model.Id;
        vehicle.BrandId = brand.Id;

        return await _vehicleRepository.AddVehicleAsync(vehicle);
    }

    public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
    {
        return await _vehicleRepository.UpdateVehicleAsync(vehicle);
    }

    public async Task<bool> DeleteVehicleAsync(int vehicleId)
    {
        return await _vehicleRepository.DeleteVehicleAsync(vehicleId);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByLicensePlateAsync(string licensePlate)
    {
        return await _vehicleRepository.GetVehiclesByLicensePlateAsync(licensePlate);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByVinAsync(string vin)
    {
        return await _vehicleRepository.GetVehiclesByVinAsync(vin);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByOwnerIdAsync(int ownerId)
    {
        return await _vehicleRepository.GetVehiclesByOwnerIdAsync(ownerId);
    }

    public async Task<IEnumerable<Vehicle>> FilterVehiclesAsync(string model, string brand, int year)
    {
        return await _vehicleRepository.FilterVehiclesAsync(model, brand, year);
    }

    public async Task<VehicleBrand?> GetVehicleBrandByNameAsync(string name)
    {
        return await _vehicleRepository.GetVehicleBrandByNameAsync(name);
    }

    public async Task<VehicleBrand> AddVehicleBrandAsync(VehicleBrand brand)
    {
        return await _vehicleRepository.AddVehicleBrandAsync(brand);
    }

    public async Task<VehicleModel?> GetVehicleModelByNameAsync(string name)
    {
        return await _vehicleRepository.GetVehicleModelByNameAsync(name);
    }

    public async Task<VehicleModel> AddVehicleModelAsync(VehicleModel model)
    {
        return await _vehicleRepository.AddVehicleModelAsync(model);
    }
}