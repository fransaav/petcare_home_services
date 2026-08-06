using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PetCare.Modules.IdentityAndPets.Domain;
using PetCare.Modules.IdentityAndPets.Infrastructure;
using PetCare.Modules.Providers.Domain;
using PetCare.Modules.Providers.Infrastructure;
using PetCare.Modules.Booking.Infrastructure;
using PetCare.Modules.Billing.Infrastructure;

namespace PetCare.API.Extensions;

public static class DatabaseSeeder
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        try
        {
            var identityDb = services.GetRequiredService<IdentityAndPetsDbContext>();
            var providersDb = services.GetRequiredService<ProvidersDbContext>();
            var bookingDb = services.GetRequiredService<BookingDbContext>();
            var billingDb = services.GetRequiredService<BillingDbContext>();

            // Aplicar migraciones o crear la BD si no existe
            // NOTA: EnsureCreated() solo crea las tablas del primer DbContext que lo ejecuta.
            // Para un Monolito Modular con múltiples DbContexts apuntando a la misma DB,
            // debemos forzar la creación de la DB primero, y luego la creación de las tablas de cada contexto.
            
            var creator = (RelationalDatabaseCreator)identityDb.Database.GetService<IDatabaseCreator>();
            if (!await creator.ExistsAsync())
            {
                await creator.CreateAsync();
            }

            try { await ((RelationalDatabaseCreator)identityDb.Database.GetService<IDatabaseCreator>()).CreateTablesAsync(); } catch { }
            try { await ((RelationalDatabaseCreator)providersDb.Database.GetService<IDatabaseCreator>()).CreateTablesAsync(); } catch { }
            try { await ((RelationalDatabaseCreator)bookingDb.Database.GetService<IDatabaseCreator>()).CreateTablesAsync(); } catch { }
            try { await ((RelationalDatabaseCreator)billingDb.Database.GetService<IDatabaseCreator>()).CreateTablesAsync(); } catch { }

            await SeedIdentityAndPetsAsync(identityDb);
            await SeedProvidersAsync(providersDb);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocurrió un error al seedear la base de datos.");
        }
    }

    private static async Task SeedIdentityAndPetsAsync(IdentityAndPetsDbContext context)
    {
        if (!context.Customers.Any())
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Pérez",
                Email = "juan.perez@example.com",
                PhoneNumber = "+59170012345",
                Address = "Av. Busch, Santa Cruz",
                Pets = new List<Pet>
                {
                    new Pet
                    {
                        Id = Guid.NewGuid(),
                        Name = "Firulais",
                        Species = "Dog",
                        Breed = "Golden Retriever",
                        WeightKg = 25.5,
                        SpecialRequirements = "Ninguno",
                        HasVaccinationRecord = true
                    }
                }
            };
            
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProvidersAsync(ProvidersDbContext context)
    {
        if (!context.Providers.Any())
        {
            var provider = new Provider
            {
                Id = Guid.NewGuid(),
                Name = "Pet Paradise Grooming",
                ServiceTypes = "Grooming, Vet",
                ServiceArea = "Santa Cruz de la Sierra",
                MaxDailyCapacity = 10,
                IsActive = true
            };
            
            context.Providers.Add(provider);
            await context.SaveChangesAsync();
        }
    }
}
