using PetCare.Modules.Booking.Domain;

namespace PetCare.Modules.Booking.Application.Interfaces;

public interface IBookingService
{
    Task<Booking?> GetBookingByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<Booking> CreateBookingAsync(Booking booking);
}
