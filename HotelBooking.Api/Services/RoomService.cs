using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Common.Exceptions;
using HotelBooking.Api.DTOs.Rooms;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repositories.Interfaces;
using HotelBooking.Api.Services.Interfaces;

namespace HotelBooking.Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<SearchRoomsRequest> _searchRoomsValidator;

        public RoomService(
            IRoomRepository roomRepository,
            IMapper mapper,
            IValidator<SearchRoomsRequest> searchRoomsValidator)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _searchRoomsValidator = searchRoomsValidator;
        }

        public async Task<IEnumerable<RoomResponse>> SearchRoomsAsync(SearchRoomsRequest request, CancellationToken cancellationToken)
        {
            await _searchRoomsValidator.ValidateAndThrowAsync(request, cancellationToken);

            IEnumerable<Room> rooms = await _roomRepository.SearchAvailableRoomsAsync(
                request.CheckIn,
                request.CheckOut,
                request.Guests,
                cancellationToken);

            return _mapper.Map<IEnumerable<RoomResponse>>(rooms);
        }

        public async Task<RoomResponse> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken)
        {
            Room? room = await _roomRepository.GetRoomByIdAsync(roomId, cancellationToken);

            // Inactive rooms are soft-deleted and should behave as if they don't exist.
            if (room is null || !room.IsActive)
            {
                throw new NotFoundException($"Room with id {roomId} was not found.");
            }

            return _mapper.Map<RoomResponse>(room);
        }
    }
}
