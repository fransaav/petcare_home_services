using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCare.Application.Interfaces.Repositories;
using PetCare.Infrastructure.Data;
using PetCare.Infrastructure.Repositories;

namespace PetCare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PetCareDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}
