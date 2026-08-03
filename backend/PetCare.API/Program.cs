using PetCare.Application;
using PetCare.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application and infrastructure services
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PetCare.Infrastructure.Data.PetCareDbContext>();
    dbContext.Database.Migrate();

    var defaultCustomerId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    if (!dbContext.Customers.Any(c => c.Id == defaultCustomerId))
    {
        var customer = new PetCare.Domain.Customers.Customer
        {
            Id = defaultCustomerId,
            FirstName = "Juan",
            LastName = "Pérez",
            Email = "juan@example.com",
            PhoneNumber = "555-1234",
            Address = "Av. Siempre Viva 123"
        };
        dbContext.Customers.Add(customer);

        dbContext.Pets.AddRange(
            new PetCare.Domain.Customers.Pet
            {
                Id = Guid.NewGuid(),
                Name = "Firulais",
                Species = "Perro",
                Breed = "Mestizo",
                WeightKg = 15.5,
                SpecialRequirements = "Ninguno",
                HasVaccinationRecord = true,
                CustomerId = defaultCustomerId
            },
            new PetCare.Domain.Customers.Pet
            {
                Id = Guid.NewGuid(),
                Name = "Mishi",
                Species = "Gato",
                Breed = "Siamés",
                WeightKg = 4.2,
                SpecialRequirements = "Dieta especial",
                HasVaccinationRecord = true,
                CustomerId = defaultCustomerId
            }
        );
        dbContext.SaveChanges();

    }

    if (!dbContext.Providers.Any())
    {
        // Seed Providers
        var provider1Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var provider2Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        dbContext.Providers.AddRange(
            new PetCare.Domain.Providers.Provider { Id = provider1Id, Name = "Pet Grooming Experts", ServiceTypes = "Grooming, Vet", ServiceArea = "Downtown", MaxDailyCapacity = 10 },
            new PetCare.Domain.Providers.Provider { Id = provider2Id, Name = "Happy Paws Walkers", ServiceTypes = "Walking, Sitting", ServiceArea = "Uptown", MaxDailyCapacity = 20 }
        );
        dbContext.SaveChanges();

        // Seed Bookings
        var firulais = dbContext.Pets.First(p => p.Name == "Firulais");
        var mishi = dbContext.Pets.First(p => p.Name == "Mishi");

        dbContext.Bookings.AddRange(
            new PetCare.Domain.Bookings.Booking { Id = Guid.NewGuid(), CustomerId = defaultCustomerId, PetId = firulais.Id, ProviderId = provider1Id, ServiceType = "Grooming", Modality = "HomeVisit", ScheduledTime = DateTime.UtcNow.AddDays(1), Status = "Pending", TotalCost = 50.00m },
            new PetCare.Domain.Bookings.Booking { Id = Guid.NewGuid(), CustomerId = defaultCustomerId, PetId = mishi.Id, ProviderId = provider2Id, ServiceType = "Sitting", Modality = "HomeVisit", ScheduledTime = DateTime.UtcNow.AddDays(2), Status = "Confirmed", TotalCost = 30.00m }
        );
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
