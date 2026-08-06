using FluentValidation;
using HotelBooking.Api.DTOs.Bookings;

namespace HotelBooking.Api.Validators
{
    public class BookingRequestValidator : AbstractValidator<BookingRequest>
    {
        public BookingRequestValidator()
        {
            RuleFor(request => request.GuestName)
                .NotEmpty().WithMessage("Guest name is required.")
                .MaximumLength(200).WithMessage("Guest name cannot exceed 200 characters.");

            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");

            RuleFor(request => request.CheckInDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Check-in date cannot be in the past.");

            RuleFor(request => request.CheckOutDate)
                .GreaterThan(request => request.CheckInDate)
                .WithMessage("Check-out date must be after the check-in date.");

            RuleFor(request => request.GuestCount)
                .GreaterThan(0)
                .WithMessage("Guest count must be at least 1.");

            RuleFor(request => request.SpecialRequests)
                .MaximumLength(1000)
                .WithMessage("Special requests cannot exceed 1000 characters.")
                .When(request => request.SpecialRequests is not null);
        }
    }
}
