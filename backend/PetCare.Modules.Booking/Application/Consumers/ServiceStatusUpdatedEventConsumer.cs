using MassTransit;
using PetCare.IntegrationEvents;
using PetCare.Modules.Booking.Application.Interfaces;

namespace PetCare.Modules.Booking.Application.Consumers;

public class ServiceStatusUpdatedEventConsumer : IConsumer<ServiceStatusUpdatedEvent>
{
    private readonly IBookingRepository _repository;

    public ServiceStatusUpdatedEventConsumer(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<ServiceStatusUpdatedEvent> context)
    {
        var message = context.Message;
        var booking = await _repository.GetByIdAsync(message.BookingId);
        
        if (booking != null)
        {
            booking.Status = message.NewStatus;
            await _repository.SaveChangesAsync();
        }
    }
}
