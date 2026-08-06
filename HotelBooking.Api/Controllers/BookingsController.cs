using HotelBooking.Api.DTOs.Bookings;
using HotelBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Creates a new booking for a room, after re-validating availability.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] BookingRequest request, CancellationToken cancellationToken)
        {
            BookingResponse booking = await _bookingService.CreateBookingAsync(request, cancellationToken);

            return CreatedAtAction(
                nameof(GetBookingByReference),
                new { reference = booking.BookingReference },
                booking);
        }

        /// <summary>
        /// Gets a booking's confirmation details by its reference number.
        /// </summary>
        [HttpGet("{reference}")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingResponse>> GetBookingByReference(string reference, CancellationToken cancellationToken)
        {
            BookingResponse booking = await _bookingService.GetBookingByReferenceAsync(reference, cancellationToken);

            return Ok(booking);
        }
    }
}
