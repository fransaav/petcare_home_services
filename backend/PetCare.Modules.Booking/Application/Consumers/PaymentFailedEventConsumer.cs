using MassTransit;
using PetCare.IntegrationEvents;
using PetCare.Modules.Booking.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace PetCare.Modules.Booking.Application.Consumers;

public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly IBookingRepository _repository;
    private readonly ILogger<PaymentFailedEventConsumer> _logger;

    public PaymentFailedEventConsumer(IBookingRepository repository, ILogger<PaymentFailedEventConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        _logger.LogWarning("Payment failed for BookingId: {BookingId}. Reason: {Reason}. Cancelling booking.", context.Message.BookingId, context.Message.Reason);
        
        var booking = await _repository.GetByIdAsync(context.Message.BookingId);
        if (booking != null)
        {
            booking.Status = "Cancelled";
            await _repository.SaveChangesAsync();
            _logger.LogInformation("BookingId {BookingId} cancelled.", context.Message.BookingId);
        }
    }
}
