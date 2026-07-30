using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCare.Modules.IdentityAndPets.Application.Interfaces;
using PetCare.Modules.IdentityAndPets.Application;
using PetCare.Modules.IdentityAndPets.Infrastructure;

namespace PetCare.Modules.IdentityAndPets;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityAndPetsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityAndPetsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
