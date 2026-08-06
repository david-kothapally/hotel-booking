namespace HotelBooking.Api.Common
{
    public class BookingReferenceGenerator : IBookingReferenceGenerator
    {
        public string Generate()
        {
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            string randomPart = Random.Shared.Next(0, 10000).ToString("D4");

            return $"HTL-{datePart}-{randomPart}";
        }
    }
}
