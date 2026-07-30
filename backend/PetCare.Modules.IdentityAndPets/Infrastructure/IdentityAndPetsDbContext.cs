using Microsoft.EntityFrameworkCore;
using PetCare.Modules.IdentityAndPets.Domain;

namespace PetCare.Modules.IdentityAndPets.Infrastructure;

public class IdentityAndPetsDbContext : DbContext
{
    public IdentityAndPetsDbContext(DbContextOptions<IdentityAndPetsDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Pet> Pets { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Pets)
            .WithOne(p => p.Customer)
            .HasForeignKey(p => p.CustomerId);
    }
}
