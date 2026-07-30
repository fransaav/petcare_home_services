namespace PetCare.Modules.IdentityAndPets.Domain;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty; // e.g., Dog, Cat
    public string Breed { get; set; } = string.Empty;
    public double WeightKg { get; set; }
    public string SpecialRequirements { get; set; } = string.Empty;
    public bool HasVaccinationRecord { get; set; }
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
