using HotelBooking.Api.DTOs.Rooms;
using HotelBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Searches for rooms that are available for the given date range and fit the guest count.
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<RoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> SearchRooms([FromQuery] SearchRoomsRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<RoomResponse> rooms = await _roomService.SearchRoomsAsync(request, cancellationToken);
            return Ok(rooms);
        }

        /// <summary>
        /// Gets the full details of a single room.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoomResponse>> GetRoomById(int id, CancellationToken cancellationToken)
        {
            RoomResponse room = await _roomService.GetRoomByIdAsync(id, cancellationToken);
            return Ok(room);
        }
    }
}
