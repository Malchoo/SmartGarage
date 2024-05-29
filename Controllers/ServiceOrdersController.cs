using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models;
using SmartGarage.Services.Contracts;

namespace SmartGarage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceOrdersController : ControllerBase
{
    private readonly IServiceOrderService _serviceOrderService;

    public ServiceOrdersController(IServiceOrderService serviceOrderService)
    {
        _serviceOrderService = serviceOrderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceOrder>>> GetServiceOrders()
    {
        var serviceOrders = await _serviceOrderService.GetAllServiceOrdersAsync();
        return Ok(serviceOrders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceOrder>> GetServiceOrder(int id)
    {
        var serviceOrder = await _serviceOrderService.GetServiceOrderByIdAsync(id);
        if (serviceOrder == null)
        {
            return NotFound();
        }
        return Ok(serviceOrder);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceOrder>> CreateServiceOrder(ServiceOrder serviceOrder)
    {
        var createdServiceOrder = await _serviceOrderService.AddServiceOrderAsync(serviceOrder);
        return CreatedAtAction(nameof(GetServiceOrder), new { id = createdServiceOrder.Id }, createdServiceOrder);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateServiceOrder(int id, ServiceOrder serviceOrder)
    {
        if (id != serviceOrder.Id)
        {
            return BadRequest();
        }

        var updatedServiceOrder = await _serviceOrderService.UpdateServiceOrderAsync(serviceOrder);

        if (updatedServiceOrder == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceOrder(int id)
    {
        var result = await _serviceOrderService.DeleteServiceOrderAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<ServiceOrder>>> GetServiceOrdersByCustomerId(int customerId, [FromQuery] int? vehicleId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
    {
        var serviceOrders = await _serviceOrderService.GetServiceOrdersByCustomerIdAsync(customerId, vehicleId, fromDate, toDate);
        return Ok(serviceOrders);
    }

    [HttpGet("vehicle/{vehicleId}/customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<ServiceOrder>>> GetServiceOrdersByVehicleAndCustomer(int vehicleId, int customerId)
    {
        var serviceOrders = await _serviceOrderService.GetServiceOrdersByVehicleAndCustomerAsync(vehicleId, customerId);
        return Ok(serviceOrders);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateServiceOrderStatus(int id, [FromBody] ServiceOrderStatus status)
    {
        var updatedServiceOrder = await _serviceOrderService.UpdateServiceOrderStatusAsync(id, status);

        if (updatedServiceOrder == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/status")]
    public async Task<ActionResult<ServiceOrderStatus>> GetServiceOrderStatus(int id)
    {
        var status = await _serviceOrderService.GetServiceOrderStatusAsync(id);
        return Ok(status);
    }

    [HttpGet("{id}/totalPrice")]
    public async Task<ActionResult<decimal>> GetTotalPrice(int id, [FromQuery] string currencyCode)
    {
        try
        {
            var totalPrice = await _serviceOrderService.CalculateTotalPriceAsync(id, currencyCode);
            return Ok(totalPrice);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}