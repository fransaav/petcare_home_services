using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetCare.Modules.Providers.Application.Interfaces;
using PetCare.Modules.Providers.Application;
using PetCare.Modules.Providers.Infrastructure;

namespace PetCare.Modules.Providers;

public static class DependencyInjection
{
    public static IServiceCollection AddProvidersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProvidersDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IProviderService, ProviderService>();

        return services;
    }
}
