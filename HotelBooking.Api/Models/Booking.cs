using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Api.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [MaxLength(20)]
        public string BookingReference { get; set; } = string.Empty;

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }

        [Required]
        [MaxLength(200)]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public int GuestCount { get; set; }

        [MaxLength(1000)]
        public string? SpecialRequests { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Confirmed";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property: the room this booking belongs to
        public Room Room { get; set; } = null!;
    }
}
