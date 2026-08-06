namespace HotelBooking.Api.DTOs.Rooms
{
    public class SearchRoomsRequest
    {
        public DateOnly CheckIn { get; set; }

        public DateOnly CheckOut { get; set; }

        public int Guests { get; set; }
    }
}
