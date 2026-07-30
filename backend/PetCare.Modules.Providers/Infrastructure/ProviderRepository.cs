using Microsoft.EntityFrameworkCore;
using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Domain;
using PetCare.Modules.Providers.Infrastructure;

namespace PetCare.Modules.Providers.Infrastructure;

public class ProviderRepository : IProviderRepository
{
    private readonly PetCareDbContext _context;

    public ProviderRepository(PetCareDbContext context)
    {
        _context = context;
    }

    public async Task<Provider> AddAsync(Provider provider)
    {
        await _context.Providers.AddAsync(provider);
        return provider;
    }

    public async Task<IEnumerable<Provider>> GetAllAsync()
    {
        return await _context.Providers.ToListAsync();
    }

    public async Task<Provider?> GetByIdAsync(Guid id)
    {
        return await _context.Providers.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
