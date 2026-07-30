using PetCare.Domain.Bookings;

namespace PetCare.Domain.Billing;

public class Payment
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
    
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed
    public string PaymentMethod { get; set; } = string.Empty; // Online, OnLocation
}
