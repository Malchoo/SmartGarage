using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models;

namespace SmartGarage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult<Vehicle>> CreateVehicle(Vehicle vehicle)
    {
        var createdVehicle = await _vehicleService.AddVehicleAsync(vehicle);
        return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.Id }, createdVehicle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, Vehicle vehicle)
    {
        if (id != vehicle.Id)
        {
            return BadRequest();
        }

        var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicle);

        if (updatedVehicle == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var result = await _vehicleService.DeleteVehicleAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Vehicle>>> SearchVehicles(string licensePlate, string vin)
    {
        IEnumerable<Vehicle> vehicles;

        if (!string.IsNullOrEmpty(licensePlate))
        {
            vehicles = await _vehicleService.GetVehiclesByLicensePlateAsync(licensePlate);
        }
        else if (!string.IsNullOrEmpty(vin))
        {
            vehicles = await _vehicleService.GetVehiclesByVinAsync(vin);
        }
        else
        {
            vehicles = await _vehicleService.GetAllVehiclesAsync();
        }

        return Ok(vehicles);
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Vehicle>>> FilterVehicles(string model, string brand, int year)
    {
        var vehicles = await _vehicleService.FilterVehiclesAsync(model, brand, year);
        return Ok(vehicles);
    }
}