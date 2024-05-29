using Microsoft.AspNetCore.Mvc;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchString, string sortOrder)
    {
        var customers = await _customerRepository.GetAllCustomersAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            customers = customers.Where(c =>
                c.User.Username.Contains(searchString) ||
                c.User.Email.Contains(searchString) ||
                c.User.PhoneNumber.Contains(searchString) ||
                c.Vehicles.Any(v => v.Model.Name.Contains(searchString) || v.Brand.Name.Contains(searchString)));
        }

        switch (sortOrder)
        {
            case "name_desc":
                customers = customers.OrderByDescending(c => c.User.Username);
                break;
            case "email":
                customers = customers.OrderBy(c => c.User.Email);
                break;
            case "email_desc":
                customers = customers.OrderByDescending(c => c.User.Email);
                break;
            default:
                customers = customers.OrderBy(c => c.User.Username);
                break;
        }

        return View(customers);
    }
}