using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models;
using SmartGarage.Services.Contracts;

namespace SmartGarage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Service>>> GetServices()
    {
        var services = await _serviceService.GetAllServicesAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetService(int id)
    {
        var service = await _serviceService.GetServiceByIdAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        return Ok(service);
    }

    [HttpPost]
    public async Task<ActionResult<Service>> CreateService(Service service)
    {
        var createdService = await _serviceService.AddServiceAsync(service);
        return CreatedAtAction(nameof(GetService), new { id = createdService.Id }, createdService);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(int id, Service service)
    {
        if (id != service.Id)
        {
            return BadRequest();
        }

        var updatedService = await _serviceService.UpdateServiceAsync(service);

        if (updatedService == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var result = await _serviceService.DeleteServiceAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Service>>> SearchServices(string name)
    {
        var services = await _serviceService.GetServicesByNameAsync(name);
        return Ok(services);
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Service>>> FilterServices(string searchString, decimal? minPrice, decimal? maxPrice)
    {
        var services = await _serviceService.FilterServicesAsync(searchString, minPrice, maxPrice);
        return Ok(services);
    }
}