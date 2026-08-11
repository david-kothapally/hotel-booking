using AutoMapper;
using FluentValidation;
using HotelBooking.Api.Common;
using HotelBooking.Api.Common.Exceptions;
using HotelBooking.Api.DTOs.Bookings;
using HotelBooking.Api.Models;
using HotelBooking.Api.Repositories.Interfaces;
using HotelBooking.Api.Services.Interfaces;

namespace HotelBooking.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingReferenceGenerator _bookingReferenceGenerator;
        private readonly IMapper _mapper;
        private readonly IValidator<BookingRequest> _bookingRequestValidator;

        public BookingService(
            IRoomRepository roomRepository,
            IBookingRepository bookingRepository,
            IBookingReferenceGenerator bookingReferenceGenerator,
            IMapper mapper,
            IValidator<BookingRequest> bookingRequestValidator)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _bookingReferenceGenerator = bookingReferenceGenerator;
            _mapper = mapper;
            _bookingRequestValidator = bookingRequestValidator;
        }

        public async Task<BookingResponse> CreateBookingAsync(BookingRequest request, CancellationToken cancellationToken)
        {
            await _bookingRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

            // 1. Validate room exists
            Room? room = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);
            if (room is null || !room.IsActive)
            {
                throw new NotFoundException($"Room with id {request.RoomId} was not found.");
            }

            // The room is already loaded here, so this capacity check costs no extra query.
            // It's a distinct failure from availability/dates, so it gets its own exception type.
            if (request.GuestCount > room.MaxGuests)
            {
                throw new GuestCountExceedsCapacityException(
                    $"Room {room.RoomNumber} allows a maximum of {room.MaxGuests} guests, but {request.GuestCount} were requested.");
            }

            // 2. Calculate total price
            int nights = request.CheckOutDate.DayNumber - request.CheckInDate.DayNumber;
            decimal totalPrice = nights * room.PricePerNight;

            // 3. Generate booking reference
            string bookingReference = _bookingReferenceGenerator.Generate();

            // 4. Save booking - availability is re-checked and the insert happens atomically,
            // inside one Serializable transaction, so a second guest booking the same room
            // concurrently can't slip through between the check and the write.
            Booking booking = new Booking
            {
                BookingReference = bookingReference,
                RoomId = room.RoomId,
                Room = room,
                GuestName = request.GuestName,
                Email = request.Email,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                GuestCount = request.GuestCount,
                SpecialRequests = request.SpecialRequests,
                TotalPrice = totalPrice,
                Status = BookingStatus.Confirmed
            };

            Booking? savedBooking = await _bookingRepository.CreateBookingIfAvailableAsync(booking, cancellationToken);

            if (savedBooking is null)
            {
                throw new RoomNotAvailableException($"Room {room.RoomNumber} is no longer available for the selected dates.");
            }

            // 5. Return BookingResponse
            return _mapper.Map<BookingResponse>(savedBooking);
        }

        public async Task<BookingResponse> GetBookingByReferenceAsync(string bookingReference, CancellationToken cancellationToken)
        {
            Booking? booking = await _bookingRepository.GetBookingByReferenceAsync(bookingReference, cancellationToken);

            if (booking is null)
            {
                throw new NotFoundException($"Booking with reference {bookingReference} was not found.");
            }

            return _mapper.Map<BookingResponse>(booking);
        }
    }
}
