using FluentValidation;
using HotelBooking.Api.DTOs.Rooms;

namespace HotelBooking.Api.Validators
{
    public class SearchRoomsRequestValidator : AbstractValidator<SearchRoomsRequest>
    {
        public SearchRoomsRequestValidator()
        {
            RuleFor(request => request.CheckIn)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Check-in date cannot be in the past.");

            RuleFor(request => request.CheckOut)
                .GreaterThan(request => request.CheckIn)
                .WithMessage("Check-out date must be after the check-in date.");

            RuleFor(request => request.Guests)
                .GreaterThan(0)
                .WithMessage("Guest count must be at least 1.");
        }
    }
}
