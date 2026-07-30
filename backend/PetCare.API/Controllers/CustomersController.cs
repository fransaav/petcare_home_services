using Microsoft.AspNetCore.Mvc;
using PetCare.Modules.IdentityAndPets.Application.Interfaces;
using PetCare.Modules.IdentityAndPets.Domain;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        var created = await _customerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("{id}/pets")]
    public async Task<IActionResult> AddPet(Guid id, [FromBody] Pet pet)
    {
        var addedPet = await _customerService.AddPetAsync(id, pet);
        return Ok(addedPet);
    }
}
