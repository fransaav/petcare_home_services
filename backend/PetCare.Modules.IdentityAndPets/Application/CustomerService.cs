using PetCare.Modules.IdentityAndPets.Application.Interfaces;
using PetCare.Modules.IdentityAndPets.Application.Interfaces;
using PetCare.Modules.IdentityAndPets.Domain;

namespace PetCare.Modules.IdentityAndPets.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer?> GetCustomerByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        customer.Id = Guid.NewGuid();
        var result = await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();
        return result;
    }

    public async Task<Pet> AddPetAsync(Guid customerId, Pet pet)
    {
        pet.Id = Guid.NewGuid();
        pet.CustomerId = customerId;
        var result = await _repository.AddPetAsync(pet);
        await _repository.SaveChangesAsync();
        return result;
    }
}
