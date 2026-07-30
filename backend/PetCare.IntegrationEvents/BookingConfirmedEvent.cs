namespace PetCare.IntegrationEvents;

public record BookingConfirmedEvent
{
    public Guid BookingId { get; init; }
    public Guid PetId { get; init; }
    public string Status { get; init; } = default!;
}
