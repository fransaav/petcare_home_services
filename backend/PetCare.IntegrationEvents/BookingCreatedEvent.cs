namespace PetCare.IntegrationEvents;

public record BookingCreatedEvent
{
    public Guid BookingId { get; init; }
    public Guid CustomerId { get; init; }
    public Guid PetId { get; init; }
    public string ServiceType { get; init; } = string.Empty;
    public decimal TotalCost { get; init; }
}
