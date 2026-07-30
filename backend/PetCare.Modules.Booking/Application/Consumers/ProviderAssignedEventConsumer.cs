using MassTransit;
using PetCare.IntegrationEvents;
using PetCare.Modules.Booking.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace PetCare.Modules.Booking.Application.Consumers;

public class ProviderAssignedEventConsumer : IConsumer<ProviderAssignedEvent>
{
    private readonly IBookingRepository _repository;
    private readonly ILogger<ProviderAssignedEventConsumer> _logger;

    public ProviderAssignedEventConsumer(IBookingRepository repository, ILogger<ProviderAssignedEventConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProviderAssignedEvent> context)
    {
        _logger.LogInformation("Provider assigned for BookingId: {BookingId}. Confirming booking.", context.Message.BookingId);
        
        var booking = await _repository.GetByIdAsync(context.Message.BookingId);
        if (booking != null)
        {
            booking.Status = "Confirmed";
            booking.ProviderId = context.Message.ProviderId;
            await _repository.SaveChangesAsync();
            _logger.LogInformation("BookingId {BookingId} successfully confirmed and assigned to Provider {ProviderId}.", context.Message.BookingId, context.Message.ProviderId);
        }
    }
}
