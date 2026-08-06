using HotelBooking.Api.Data;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _context;

        public BookingRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);

            return booking;
        }

        public async Task<Booking?> GetBookingByReferenceAsync(string bookingReference, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Include(booking => booking.Room)
                .FirstOrDefaultAsync(booking => booking.BookingReference == bookingReference, cancellationToken);
        }

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateOnly checkIn, DateOnly checkOut, CancellationToken cancellationToken)
        {
            bool hasConflict = await _context.Bookings.AnyAsync(booking =>
                booking.RoomId == roomId &&
                booking.Status == "Confirmed" &&
                booking.CheckInDate < checkOut &&
                booking.CheckOutDate > checkIn,
                cancellationToken);

            return !hasConflict;
        }
    }
}
