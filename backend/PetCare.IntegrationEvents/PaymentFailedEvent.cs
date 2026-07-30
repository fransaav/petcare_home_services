namespace PetCare.IntegrationEvents;

public record PaymentFailedEvent
{
    public Guid BookingId { get; init; }
    public string Reason { get; init; } = string.Empty;
}
