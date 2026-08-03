namespace PetCare.Domain.Providers;

public class Provider
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ServiceTypes { get; set; } = string.Empty; // e.g., "Grooming, Walking, Vet"
    public string ServiceArea { get; set; } = string.Empty;
    public int MaxDailyCapacity { get; set; }
    public bool IsActive { get; set; } = true;
}
