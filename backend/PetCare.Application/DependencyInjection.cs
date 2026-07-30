using Microsoft.Extensions.DependencyInjection;
using PetCare.Application.Interfaces;
using PetCare.Application.Services;

namespace PetCare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProviderService, ProviderService>();
        services.AddScoped<IBookingService, BookingService>();
        
        return services;
    }
}
