namespace PetCare.IntegrationEvents;

public record ServiceStatusUpdatedEvent
{
    public Guid BookingId { get; init; }
    public string NewStatus { get; init; } = default!;
}
