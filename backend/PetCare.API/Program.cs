using PetCare.API.Extensions;
using PetCare.Modules.IdentityAndPets;
using PetCare.Modules.Booking;
using PetCare.Modules.Providers;
using PetCare.Modules.Billing;
using MassTransit;
using PetCare.Modules.Booking.Application.Consumers;
using PetCare.Modules.Billing.Application.Consumers;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

// Register Modules
builder.Services.AddIdentityAndPetsModule(builder.Configuration);
builder.Services.AddBookingModule(builder.Configuration);
builder.Services.AddProvidersModule(builder.Configuration);
builder.Services.AddBillingModule(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ServiceStatusUpdatedEventConsumer>();
    x.AddConsumer<BookingConfirmedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("ServiceStatusUpdatedQueue", e =>
        {
            e.ConfigureConsumer<ServiceStatusUpdatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint("BookingConfirmedQueue", e =>
        {
            e.ConfigureConsumer<BookingConfirmedEventConsumer>(context);
        });
    });
});


var app = builder.Build();

// Inicializar la base de datos con datos de prueba
await app.SeedDataAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
