using HotelBooking.Api.DTOs.Bookings;

namespace HotelBooking.Api.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateBookingAsync(BookingRequest request, CancellationToken cancellationToken);

        Task<BookingResponse> GetBookingByReferenceAsync(string bookingReference, CancellationToken cancellationToken);
    }
}
