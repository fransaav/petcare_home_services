using PetCare.Modules.Providers.Domain;

namespace PetCare.Modules.Providers.Application.Interfaces;

public interface IProviderRepository
{
    Task<Provider?> GetByIdAsync(Guid id);
    Task<IEnumerable<Provider>> GetAllAsync();
    Task<Provider> AddAsync(Provider provider);
    Task SaveChangesAsync();
}
