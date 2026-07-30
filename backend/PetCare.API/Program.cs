using PetCare.Modules.IdentityAndPets;
using PetCare.Modules.Booking;
using PetCare.Modules.Providers;
using PetCare.Modules.Billing;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
