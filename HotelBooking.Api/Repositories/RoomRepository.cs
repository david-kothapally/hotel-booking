using HotelBooking.Api.Common;
using HotelBooking.Api.Data;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _context;

        public RoomRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> SearchAvailableRoomsAsync(DateOnly checkIn, DateOnly checkOut, int guests, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .Where(room => room.IsActive)
                .Where(room => room.MaxGuests >= guests)
                .Where(room => !room.Bookings.Any(booking =>
                    booking.Status == BookingStatus.Confirmed &&
                    booking.CheckInDate < checkOut &&
                    booking.CheckOutDate > checkIn))
                .ToListAsync(cancellationToken);
        }

        public async Task<Room?> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(room => room.RoomId == roomId, cancellationToken);
        }
    }
}
