using MassTransit;
using PetCare.IntegrationEvents;
using PetCare.Modules.Booking.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace PetCare.Modules.Booking.Application.Consumers;

public class ProviderAssignmentFailedEventConsumer : IConsumer<ProviderAssignmentFailedEvent>
{
    private readonly IBookingRepository _repository;
    private readonly ILogger<ProviderAssignmentFailedEventConsumer> _logger;

    public ProviderAssignmentFailedEventConsumer(IBookingRepository repository, ILogger<ProviderAssignmentFailedEventConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProviderAssignmentFailedEvent> context)
    {
        _logger.LogWarning("Provider assignment failed for BookingId: {BookingId}. Reason: {Reason}. Cancelling booking.", context.Message.BookingId, context.Message.Reason);
        
        var booking = await _repository.GetByIdAsync(context.Message.BookingId);
        if (booking != null)
        {
            booking.Status = "Cancelled";
            await _repository.SaveChangesAsync();
            _logger.LogInformation("BookingId {BookingId} cancelled due to provider unavailability.", context.Message.BookingId);
            
            // In a real system, we'd also emit a BookingCancelledEvent so Billing could refund the payment
        }
    }
}
