using HotelBooking.Api.Models;

namespace HotelBooking.Api.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> SearchAvailableRoomsAsync(DateOnly checkIn, DateOnly checkOut, int guests, CancellationToken cancellationToken);

        Task<Room?> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken);
    }
}
