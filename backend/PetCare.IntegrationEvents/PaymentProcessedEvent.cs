namespace PetCare.IntegrationEvents;

public record PaymentProcessedEvent
{
    public Guid BookingId { get; init; }
}
