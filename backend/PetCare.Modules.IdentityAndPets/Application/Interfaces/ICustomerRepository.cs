using PetCare.Modules.IdentityAndPets.Domain;

namespace PetCare.Modules.IdentityAndPets.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> AddAsync(Customer customer);
    Task<Pet> AddPetAsync(Pet pet);
    Task SaveChangesAsync();
}
