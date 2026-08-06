using HotelBooking.Api.Models;

namespace HotelBooking.Api.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBookingAsync(Booking booking, CancellationToken cancellationToken);

        Task<Booking?> GetBookingByReferenceAsync(string bookingReference, CancellationToken cancellationToken);

        Task<bool> IsRoomAvailableAsync(int roomId, DateOnly checkIn, DateOnly checkOut, CancellationToken cancellationToken);
    }
}
