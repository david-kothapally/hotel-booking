namespace HotelBooking.Api.Common.Exceptions
{
    public class RoomNotAvailableException : Exception
    {
        public RoomNotAvailableException()
        {
        }

        public RoomNotAvailableException(string message)
            : base(message)
        {
        }

        public RoomNotAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
