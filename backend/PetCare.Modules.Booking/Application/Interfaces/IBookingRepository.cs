using PetCare.Modules.Booking.Domain;

namespace PetCare.Modules.Booking.Application.Interfaces;

public interface IBookingRepository
{
    Task<Domain.Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Domain.Booking>> GetAllAsync();
    Task<Domain.Booking> AddAsync(Domain.Booking booking);
    Task SaveChangesAsync();
}
