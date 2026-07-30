namespace PetCare.IntegrationEvents;

public record ProviderAssignedEvent
{
    public Guid BookingId { get; init; }
    public Guid ProviderId { get; init; }
}
