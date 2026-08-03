using PetCare.Domain.Bookings;

namespace PetCare.Application.Interfaces;

public interface IBookingService
{
    Task<Booking?> GetBookingByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetAllBookingsAsync();
    Task<Booking> CreateBookingAsync(Booking booking);
}
