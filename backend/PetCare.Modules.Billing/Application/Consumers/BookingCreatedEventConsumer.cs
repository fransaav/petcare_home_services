using MassTransit;
using PetCare.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace PetCare.Modules.Billing.Application.Consumers;

public class BookingCreatedEventConsumer : IConsumer<BookingCreatedEvent>
{
    private readonly ILogger<BookingCreatedEventConsumer> _logger;

    public BookingCreatedEventConsumer(ILogger<BookingCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BookingCreatedEvent> context)
    {
        _logger.LogInformation("Processing payment for BookingId: {BookingId}...", context.Message.BookingId);

        // Simulate payment logic. Reject if Cost > 1000
        if (context.Message.TotalCost > 1000)
        {
            _logger.LogWarning("Payment failed for BookingId: {BookingId}. Insufficient funds.", context.Message.BookingId);
            await context.Publish(new PaymentFailedEvent
            {
                BookingId = context.Message.BookingId,
                Reason = "Insufficient funds"
            });
            return;
        }

        // Simulating successful payment processing
        _logger.LogInformation("Payment processed successfully for BookingId: {BookingId}", context.Message.BookingId);
        await context.Publish(new PaymentProcessedEvent
        {
            BookingId = context.Message.BookingId
        });
    }
}
