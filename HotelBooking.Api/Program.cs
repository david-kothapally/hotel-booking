using FluentValidation;
using HotelBooking.Api.Common;
using HotelBooking.Api.Data;
using HotelBooking.Api.Middleware;
using HotelBooking.Api.Repositories;
using HotelBooking.Api.Repositories.Interfaces;
using HotelBooking.Api.Services;
using HotelBooking.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string ClientOrigin = "AllowClient";

// Add services to the container.

builder.Services.AddCors(options =>
{
    // Allows the Vite dev server to call this API. Vite defaults to 5173
    // but falls back to 5174+ if that port is already taken.
    options.AddPolicy(ClientOrigin, policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(config => { }, typeof(Program).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddSingleton<IBookingReferenceGenerator, BookingReferenceGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Registered first so it wraps every other middleware below and can
// catch exceptions thrown anywhere further down the pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(ClientOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
