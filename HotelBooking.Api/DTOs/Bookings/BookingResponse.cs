namespace HotelBooking.Api.DTOs.Bookings
{
    public class BookingResponse
    {
        public string BookingReference { get; set; } = string.Empty;

        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public string RoomType { get; set; } = string.Empty;

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public int GuestCount { get; set; }

        public string GuestName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? SpecialRequests { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
