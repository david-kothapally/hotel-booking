namespace HotelBooking.Api.DTOs.Rooms
{
    public class RoomResponse
    {
        public int RoomId { get; set; }

        public string RoomNumber { get; set; } = string.Empty;

        public string RoomType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal PricePerNight { get; set; }

        public int MaxGuests { get; set; }

        public string[] Amenities { get; set; } = Array.Empty<string>();

        public string ImageUrl { get; set; } = string.Empty;
    }
}
