using PetCare.Modules.Providers.Domain;

namespace PetCare.Modules.Providers.Application.Interfaces;

public interface IProviderService
{
    Task<Provider?> GetProviderByIdAsync(Guid id);
    Task<IEnumerable<Provider>> GetAllProvidersAsync();
    Task<Provider> CreateProviderAsync(Provider provider);
    Task UpdateServiceStatusAsync(Guid bookingId, string newStatus);
}
