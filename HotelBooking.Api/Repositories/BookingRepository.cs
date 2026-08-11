using System.Data;
using HotelBooking.Api.Common;
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

        public async Task<Booking?> CreateBookingIfAvailableAsync(Booking booking, CancellationToken cancellationToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

            bool hasConflict = await _context.Bookings.AnyAsync(existingBooking =>
                existingBooking.RoomId == booking.RoomId &&
                existingBooking.Status == BookingStatus.Confirmed &&
                existingBooking.CheckInDate < booking.CheckOutDate &&
                existingBooking.CheckOutDate > booking.CheckInDate,
                cancellationToken);

            if (hasConflict)
            {
                await transaction.RollbackAsync(cancellationToken);
                return null;
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return booking;
        }

        public async Task<Booking?> GetBookingByReferenceAsync(string bookingReference, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Include(booking => booking.Room)
                .FirstOrDefaultAsync(booking => booking.BookingReference == bookingReference, cancellationToken);
        }
    }
}
