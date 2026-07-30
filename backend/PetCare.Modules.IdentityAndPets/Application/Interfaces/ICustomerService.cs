using PetCare.Modules.IdentityAndPets.Domain;

namespace PetCare.Modules.IdentityAndPets.Application.Interfaces;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Pet> AddPetAsync(Guid customerId, Pet pet);
}
