using HotelBooking.Api.Models;

namespace HotelBooking.Api.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> CreateBookingIfAvailableAsync(Booking booking, CancellationToken cancellationToken);

        Task<Booking?> GetBookingByReferenceAsync(string bookingReference, CancellationToken cancellationToken);
    }
}
