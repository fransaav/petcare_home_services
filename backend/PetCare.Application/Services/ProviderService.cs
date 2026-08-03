using PetCare.Application.Interfaces;
using PetCare.Application.Interfaces.Repositories;
using PetCare.Domain.Providers;

namespace PetCare.Application.Services;

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
