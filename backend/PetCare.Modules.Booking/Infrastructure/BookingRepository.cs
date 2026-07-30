using Microsoft.EntityFrameworkCore;
using PetCare.Modules.Booking.Application.Interfaces;
using PetCare.Modules.Booking.Domain;
using PetCare.Modules.Booking.Infrastructure;

namespace PetCare.Modules.Booking.Infrastructure;

public class BookingRepository : IBookingRepository
{
    private readonly BookingDbContext _context;

    public BookingRepository(BookingDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Booking> AddAsync(Domain.Booking booking)
    {
        _context.Bookings.Add(booking);
        return await Task.FromResult(booking);
    }

    public async Task<IEnumerable<Domain.Booking>> GetAllAsync()
    {
        return await _context.Bookings
            .Include(b => b.Customer)
            .Include(b => b.Pet)
            .Include(b => b.Provider)
            .ToListAsync();
    }

    public async Task<Domain.Booking?> GetByIdAsync(Guid id)
    {
        return await _context.Bookings
            .Include(b => b.Customer)
            .Include(b => b.Pet)
            .Include(b => b.Provider)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
