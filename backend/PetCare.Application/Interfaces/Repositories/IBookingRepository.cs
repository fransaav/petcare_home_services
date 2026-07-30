using PetCare.Domain.Bookings;

namespace PetCare.Application.Interfaces.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking> AddAsync(Booking booking);
    Task SaveChangesAsync();
}
