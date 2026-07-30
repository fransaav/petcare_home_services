using MassTransit;
using PetCare.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace PetCare.Modules.Billing.Application.Consumers;

public class BookingConfirmedEventConsumer : IConsumer<BookingConfirmedEvent>
{
    private readonly ILogger<BookingConfirmedEventConsumer> _logger;

    public BookingConfirmedEventConsumer(ILogger<BookingConfirmedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<BookingConfirmedEvent> context)
    {
        _logger.LogInformation("Booking confirmed event received. Proceeding to create invoice for BookingId: {BookingId}", context.Message.BookingId);
        return Task.CompletedTask;
    }
}
