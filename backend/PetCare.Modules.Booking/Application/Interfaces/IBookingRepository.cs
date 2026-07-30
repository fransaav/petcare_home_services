using PetCare.Modules.Booking.Domain;

namespace PetCare.Modules.Booking.Application.Interfaces;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking> AddAsync(Booking booking);
    Task SaveChangesAsync();
}
