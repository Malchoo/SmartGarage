using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Repostiories;

public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId)
    {
        return await _context.Vehicles.FindAsync(vehicleId);
    }

    public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }

    public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
    {
        _context.Entry(vehicle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return vehicle;
    }

    public async Task<bool> DeleteVehicleAsync(int vehicleId)
    {
        var vehicle = await _context.Vehicles.FindAsync(vehicleId);
        if (vehicle == null)
            return false;

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByLicensePlateAsync(string licensePlate)
    {
        return await _context.Vehicles.Where(v => v.LicensePlate == licensePlate).ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByVinAsync(string vin)
    {
        return await _context.Vehicles.Where(v => v.VIN == vin).ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesByOwnerIdAsync(int ownerId)
    {
        return await _context.Vehicles.Where(v => v.CustomerId == ownerId).ToListAsync();
    }

    public async Task<IEnumerable<Vehicle>> FilterVehiclesAsync(string model, string brand, int year)
    {
        var query = _context.Vehicles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(model))
            query = query.Where(v => v.Model.Name == model);

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(v => v.Brand.Name == brand);

        if (year > 0)
            query = query.Where(v => v.YearOfCreation == year);

        return await query.ToListAsync();
    }

    public async Task<VehicleBrand?> GetVehicleBrandByNameAsync(string name)
    {
        return await _context.VehicleBrands.FirstOrDefaultAsync(b => b.Name == name);
    }

    public async Task<VehicleBrand> AddVehicleBrandAsync(VehicleBrand brand)
    {
        _context.VehicleBrands.Add(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<VehicleModel?> GetVehicleModelByNameAsync(string name)
    {
        return await _context.VehicleModels.FirstOrDefaultAsync(m => m.Name == name);
    }

    public async Task<VehicleModel> AddVehicleModelAsync(VehicleModel model)
    {
        _context.VehicleModels.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }
}