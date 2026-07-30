using Microsoft.EntityFrameworkCore;
using PetCare.Modules.IdentityAndPets.Application.Interfaces;
using PetCare.Modules.IdentityAndPets.Domain;
using PetCare.Modules.IdentityAndPets.Infrastructure;

namespace PetCare.Modules.IdentityAndPets.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    private readonly PetCareDbContext _context;

    public CustomerRepository(PetCareDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
        return customer;
    }

    public async Task<Pet> AddPetAsync(Pet pet)
    {
        await _context.Pets.AddAsync(pet);
        return pet;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.Include(c => c.Pets).ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers.Include(c => c.Pets).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
