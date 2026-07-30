using Microsoft.EntityFrameworkCore;
using PetCare.Modules.Booking.Domain;
using PetCare.Modules.IdentityAndPets.Domain;
using PetCare.Modules.Providers.Domain;

namespace PetCare.Modules.Booking.Infrastructure;

public class BookingDbContext : DbContext
{
    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

    public DbSet<PetCare.Modules.Booking.Domain.Booking> Bookings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("booking");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PetCare.Modules.Booking.Domain.Booking>()
            .HasOne(b => b.Customer)
            .WithMany()
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PetCare.Modules.Booking.Domain.Booking>()
            .HasOne(b => b.Pet)
            .WithMany()
            .HasForeignKey(b => b.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PetCare.Modules.Booking.Domain.Booking>()
            .HasOne(b => b.Provider)
            .WithMany()
            .HasForeignKey(b => b.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
