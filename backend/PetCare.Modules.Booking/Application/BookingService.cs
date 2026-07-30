using PetCare.Modules.Booking.Application.Interfaces;
using PetCare.Modules.Booking.Application.Interfaces;
using PetCare.Modules.Booking.Domain;

namespace PetCare.Modules.Booking.Application;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;

    public BookingService(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Booking?> GetBookingByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Booking> CreateBookingAsync(Booking booking)
    {
        booking.Id = Guid.NewGuid();
        booking.Status = "Pending";
        var result = await _repository.AddAsync(booking);
        await _repository.SaveChangesAsync();
        return result;
    }
}
