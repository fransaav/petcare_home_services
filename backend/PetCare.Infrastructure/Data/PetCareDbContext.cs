using Microsoft.EntityFrameworkCore;
using PetCare.Domain.Billing;
using PetCare.Domain.Bookings;
using PetCare.Domain.Customers;
using PetCare.Domain.Providers;

namespace PetCare.Infrastructure.Data;

public class PetCareDbContext : DbContext
{
    public PetCareDbContext(DbContextOptions<PetCareDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Pet> Pets { get; set; } = null!;
    public DbSet<Provider> Providers { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Simple configuration for minimal MVP
        
        // Customer - Pet (One to Many)
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Pets)
            .WithOne(p => p.Customer)
            .HasForeignKey(p => p.CustomerId);

        // Booking relations
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Customer)
            .WithMany()
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Pet)
            .WithMany()
            .HasForeignKey(b => b.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Provider)
            .WithMany()
            .HasForeignKey(b => b.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Payment - Booking (One to One/Many)
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Booking)
            .WithMany()
            .HasForeignKey(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
