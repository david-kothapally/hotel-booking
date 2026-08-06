namespace HotelBooking.Api.Common.Exceptions
{
    public class GuestCountExceedsCapacityException : Exception
    {
        public GuestCountExceedsCapacityException()
        {
        }

        public GuestCountExceedsCapacityException(string message)
            : base(message)
        {
        }

        public GuestCountExceedsCapacityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
