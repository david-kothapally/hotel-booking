using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HotelBooking.Api.Common.Exceptions;
using HotelBooking.Api.DTOs.Rooms;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repositories.Interfaces;
using HotelBooking.Api.Services;
using Moq;
using Xunit;

namespace HotelBooking.Api.Tests.Services
{
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _roomRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IValidator<SearchRoomsRequest>> _validatorMock = new();

        private RoomService CreateService()
        {
            return new RoomService(_roomRepositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetRoomByIdAsync_WhenRoomExists_ReturnsMappedRoomResponse()
        {
            Room room = new Room { RoomId = 1, RoomNumber = "101", RoomType = "Standard King", IsActive = true };
            RoomResponse expectedResponse = new RoomResponse { RoomId = 1, RoomNumber = "101", RoomType = "Standard King" };

            _roomRepositoryMock
                .Setup(r => r.GetRoomByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);

            _mapperMock
                .Setup(m => m.Map<RoomResponse>(room))
                .Returns(expectedResponse);

            RoomService service = CreateService();

            RoomResponse result = await service.GetRoomByIdAsync(1, CancellationToken.None);

            Assert.Equal(expectedResponse.RoomId, result.RoomId);
            Assert.Equal(expectedResponse.RoomNumber, result.RoomNumber);
        }

        [Fact]
        public async Task GetRoomByIdAsync_WhenRoomDoesNotExist_ThrowsNotFoundException()
        {
            _roomRepositoryMock
                .Setup(r => r.GetRoomByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Room?)null);

            RoomService service = CreateService();

            await Assert.ThrowsAsync<NotFoundException>(() => service.GetRoomByIdAsync(1, CancellationToken.None));
        }

        [Fact]
        public async Task GetRoomByIdAsync_WhenRoomIsInactive_ThrowsNotFoundException()
        {
            Room inactiveRoom = new Room { RoomId = 1, RoomNumber = "101", RoomType = "Standard King", IsActive = false };

            _roomRepositoryMock
                .Setup(r => r.GetRoomByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(inactiveRoom);

            RoomService service = CreateService();

            await Assert.ThrowsAsync<NotFoundException>(() => service.GetRoomByIdAsync(1, CancellationToken.None));
        }

        [Fact]
        public async Task SearchRoomsAsync_WhenRequestIsValid_ReturnsMappedRooms()
        {
            SearchRoomsRequest request = new SearchRoomsRequest
            {
                CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(3)),
                Guests = 2
            };

            List<Room> rooms = new List<Room>
            {
                new Room { RoomId = 1, RoomNumber = "101", RoomType = "Standard King", IsActive = true }
            };

            List<RoomResponse> expectedResponses = new List<RoomResponse>
            {
                new RoomResponse { RoomId = 1, RoomNumber = "101", RoomType = "Standard King" }
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _roomRepositoryMock
                .Setup(r => r.SearchAvailableRoomsAsync(request.CheckIn, request.CheckOut, request.Guests, It.IsAny<CancellationToken>()))
                .ReturnsAsync(rooms);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<RoomResponse>>(rooms))
                .Returns(expectedResponses);

            RoomService service = CreateService();

            IEnumerable<RoomResponse> result = await service.SearchRoomsAsync(request, CancellationToken.None);

            Assert.Single(result);
        }


    }
}
