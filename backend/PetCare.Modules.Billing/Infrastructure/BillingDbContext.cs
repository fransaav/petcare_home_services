using Microsoft.EntityFrameworkCore;
using PetCare.Modules.Billing.Domain;

namespace PetCare.Modules.Billing.Infrastructure;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("billing");
        base.OnModelCreating(modelBuilder);
    }
}
