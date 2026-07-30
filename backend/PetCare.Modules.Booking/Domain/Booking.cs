using PetCare.Modules.IdentityAndPets.Domain;
using PetCare.Modules.Providers.Domain;

namespace PetCare.Modules.Booking.Domain;

public class Booking
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    
    public Guid PetId { get; set; }
    public Pet Pet { get; set; } = null!;
    
    public Guid ProviderId { get; set; }
    public Provider Provider { get; set; } = null!;

    public string ServiceType { get; set; } = string.Empty; // e.g. Grooming, Walking, Vet
    public string Modality { get; set; } = string.Empty; // HomeVisit, PickupDropoff
    
    public DateTime ScheduledTime { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
    
    public decimal TotalCost { get; set; }
}
