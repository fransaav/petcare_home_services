using Microsoft.AspNetCore.Mvc;
using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Domain;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProvidersController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var providers = await _providerService.GetAllProvidersAsync();
        return Ok(providers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var provider = await _providerService.GetProviderByIdAsync(id);
        if (provider == null) return NotFound();
        return Ok(provider);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Provider provider)
    {
        var created = await _providerService.CreateProviderAsync(provider);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("update-booking-status")]
    public async Task<IActionResult> UpdateBookingStatus([FromQuery] Guid bookingId, [FromQuery] string newStatus)
    {
        await _providerService.UpdateServiceStatusAsync(bookingId, newStatus);
        return Ok(new { Message = "Service status updated and event published." });
    }
}
