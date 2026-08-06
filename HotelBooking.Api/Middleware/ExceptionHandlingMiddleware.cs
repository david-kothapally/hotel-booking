using System.Text.Json;
using FluentValidation;
using HotelBooking.Api.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ProblemDetails problemDetails;

            if (exception is NotFoundException)
            {
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Resource not found",
                    Detail = exception.Message
                };
            }
            else if (exception is RoomNotAvailableException)
            {
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Room not available",
                    Detail = exception.Message
                };
            }
            else if (exception is GuestCountExceedsCapacityException)
            {
                // Client sent a guest count the chosen room can't hold - a bad
                // request, not a conflict with server state, so this is a 400.
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Guest count exceeds room capacity",
                    Detail = exception.Message
                };
            }
            else if (exception is ValidationException validationException)
            {
                problemDetails = BuildValidationProblemDetails(validationException);
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception occurred.");

                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred",
                    Detail = "Please try again later."
                };
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;

            string json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }

        private static ValidationProblemDetails BuildValidationProblemDetails(ValidationException validationException)
        {
            Dictionary<string, string[]> errors = validationException.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key, group => group.Select(error => error.ErrorMessage).ToArray());

            return new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred."
            };
        }
    }
}
