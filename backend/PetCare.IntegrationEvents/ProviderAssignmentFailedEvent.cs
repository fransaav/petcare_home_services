namespace PetCare.IntegrationEvents;

public record ProviderAssignmentFailedEvent
{
    public Guid BookingId { get; init; }
    public string Reason { get; init; } = string.Empty;
}
