using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Domain;

namespace PetCare.Modules.Providers.Application;

public class ProviderService : IProviderService
{
    private readonly IProviderRepository _repository;

    public ProviderService(IProviderRepository repository)
    {
        _repository = repository;
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
}
