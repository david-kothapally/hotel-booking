using HotelBooking.Api.DTOs.Rooms;

namespace HotelBooking.Api.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponse>> SearchRoomsAsync(SearchRoomsRequest request, CancellationToken cancellationToken);

        Task<RoomResponse> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken);
    }
}
