using Microsoft.EntityFrameworkCore;
using PetCare.Application.Interfaces.Repositories;
using PetCare.Domain.Customers;
using PetCare.Infrastructure.Data;

namespace PetCare.Infrastructure.Repositories;

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
