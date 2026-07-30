using MassTransit;
using PetCare.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace PetCare.Modules.Providers.Application.Consumers;

public class PaymentProcessedEventConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly ILogger<PaymentProcessedEventConsumer> _logger;

    public PaymentProcessedEventConsumer(ILogger<PaymentProcessedEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        _logger.LogInformation("Attempting to assign provider for BookingId: {BookingId}...", context.Message.BookingId);

        // Simulate provider assignment logic.
        // For simplicity, we just assign a random GUID, but we simulate a 10% rejection.
        var rand = new Random();
        if (rand.Next(1, 100) <= 10)
        {
            _logger.LogWarning("Provider assignment failed for BookingId: {BookingId}. No availability.", context.Message.BookingId);
            await context.Publish(new ProviderAssignmentFailedEvent
            {
                BookingId = context.Message.BookingId,
                Reason = "No providers available at the requested time"
            });
            return;
        }

        var providerId = Guid.NewGuid(); // Simulating assigned provider
        _logger.LogInformation("Provider {ProviderId} assigned for BookingId: {BookingId}", providerId, context.Message.BookingId);
        
        await context.Publish(new ProviderAssignedEvent
        {
            BookingId = context.Message.BookingId,
            ProviderId = providerId
        });
    }
}
