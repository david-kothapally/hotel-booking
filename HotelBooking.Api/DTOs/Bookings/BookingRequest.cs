namespace HotelBooking.Api.DTOs.Bookings
{
    public class BookingRequest
    {
        public int RoomId { get; set; }

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public int GuestCount { get; set; }

        public string GuestName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? SpecialRequests { get; set; }
    }
}
