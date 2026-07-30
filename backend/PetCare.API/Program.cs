using PetCare.Modules.IdentityAndPets;
using PetCare.Modules.Booking;
using PetCare.Modules.Providers;
using PetCare.Modules.Billing;
using MassTransit;
using PetCare.Modules.Booking.Application.Consumers;
using PetCare.Modules.Billing.Application.Consumers;
using PetCare.Modules.Providers.Application.Consumers;
using System.Text.Json.Serialization;

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
    // Existing
    x.AddConsumer<ServiceStatusUpdatedEventConsumer>();
    x.AddConsumer<BookingConfirmedEventConsumer>();

    // New Saga (Choreography) Consumers
    x.AddConsumer<BookingCreatedEventConsumer>();
    x.AddConsumer<PaymentProcessedEventConsumer>();
    x.AddConsumer<PaymentFailedEventConsumer>();
    x.AddConsumer<ProviderAssignedEventConsumer>();
    x.AddConsumer<ProviderAssignmentFailedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
