using PetCare.Domain.Customers;

namespace PetCare.Application.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> AddAsync(Customer customer);
    Task<Pet> AddPetAsync(Pet pet);
    Task SaveChangesAsync();
}
