using PetCare.Domain.Providers;

namespace PetCare.Application.Interfaces.Repositories;

public interface IProviderRepository
{
    Task<Provider?> GetByIdAsync(Guid id);
    Task<IEnumerable<Provider>> GetAllAsync();
    Task<Provider> AddAsync(Provider provider);
    Task SaveChangesAsync();
}
