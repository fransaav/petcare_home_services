using Microsoft.EntityFrameworkCore;
using PetCare.Application.Interfaces.Repositories;
using PetCare.Domain.Providers;
using PetCare.Infrastructure.Data;

namespace PetCare.Infrastructure.Repositories;

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
