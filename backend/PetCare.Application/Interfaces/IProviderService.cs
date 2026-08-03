using PetCare.Domain.Providers;

namespace PetCare.Application.Interfaces;

public interface IProviderService
{
    Task<Provider?> GetProviderByIdAsync(Guid id);
    Task<IEnumerable<Provider>> GetAllProvidersAsync();
    Task<Provider> CreateProviderAsync(Provider provider);
}
