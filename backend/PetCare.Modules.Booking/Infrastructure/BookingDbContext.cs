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
    }
}
