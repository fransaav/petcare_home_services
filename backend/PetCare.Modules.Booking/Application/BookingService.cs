using PetCare.Modules.Booking.Application.Interfaces;
using PetCare.Modules.Booking.Application.Interfaces;
using PetCare.Modules.Booking.Domain;
using MassTransit;
using PetCare.IntegrationEvents;

namespace PetCare.Modules.Booking.Application;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public BookingService(IBookingRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Domain.Booking?> GetBookingByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Domain.Booking>> GetAllBookingsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Domain.Booking> CreateBookingAsync(Domain.Booking booking)
    {
        booking.Id = Guid.NewGuid();
        booking.Status = "Pending";
        var result = await _repository.AddAsync(booking);
        await _repository.SaveChangesAsync();

        await _publishEndpoint.Publish(new BookingCreatedEvent
        {
            BookingId = booking.Id,
            CustomerId = booking.CustomerId,
            PetId = booking.PetId,
            ServiceType = booking.ServiceType,
            TotalCost = booking.TotalCost
        });

        return result;
    }

    public async Task ConfirmBookingAsync(Guid id)
    {
        var booking = await _repository.GetByIdAsync(id);
        if (booking != null)
        {
            booking.Status = "Confirmed";
            await _repository.SaveChangesAsync();

            await _publishEndpoint.Publish(new BookingConfirmedEvent
            {
                BookingId = booking.Id,
                PetId = booking.PetId,
                Status = booking.Status
            });
        }
    }
}
