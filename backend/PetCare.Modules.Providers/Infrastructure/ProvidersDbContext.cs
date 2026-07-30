using Microsoft.EntityFrameworkCore;
using PetCare.Modules.Providers.Domain;

namespace PetCare.Modules.Providers.Infrastructure;

public class ProvidersDbContext : DbContext
{
    public ProvidersDbContext(DbContextOptions<ProvidersDbContext> options) : base(options) { }

    public DbSet<Provider> Providers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("providers");
        base.OnModelCreating(modelBuilder);
    }
}
