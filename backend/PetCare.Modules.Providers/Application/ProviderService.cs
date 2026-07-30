using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Domain;
using MassTransit;
using PetCare.IntegrationEvents;

namespace PetCare.Modules.Providers.Application;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProviderService(IProviderRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Provider?> GetProviderByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Provider>> GetAllProvidersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Provider> CreateProviderAsync(Provider provider)
    {
        provider.Id = Guid.NewGuid();
        var result = await _repository.AddAsync(provider);
        await _repository.SaveChangesAsync();
        return result;
    }

    public async Task UpdateServiceStatusAsync(Guid bookingId, string newStatus)
    {
        // Provider updates the service status
        await _publishEndpoint.Publish(new ServiceStatusUpdatedEvent
        {
            BookingId = bookingId,
            NewStatus = newStatus
        });
    }
}
