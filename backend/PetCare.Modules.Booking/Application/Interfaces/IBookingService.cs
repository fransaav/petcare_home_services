using PetCare.Modules.Booking.Domain;

namespace PetCare.Modules.Booking.Application.Interfaces;

public interface IBookingService
{
    Task<Domain.Booking?> GetBookingByIdAsync(Guid id);
    Task<IEnumerable<Domain.Booking>> GetAllBookingsAsync();
    Task<Domain.Booking> CreateBookingAsync(Domain.Booking booking);
    Task ConfirmBookingAsync(Guid id);
}
