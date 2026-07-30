using Microsoft.EntityFrameworkCore;
using PetCare.Application.Interfaces.Repositories;
using PetCare.Domain.Bookings;
using PetCare.Infrastructure.Data;

namespace PetCare.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly PetCareDbContext _context;

    public BookingRepository(PetCareDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> AddAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
        return booking;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings
            .Include(b => b.Customer)
            .Include(b => b.Pet)
            .Include(b => b.Provider)
            .ToListAsync();
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
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
