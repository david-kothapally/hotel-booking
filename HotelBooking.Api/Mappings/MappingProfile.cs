using AutoMapper;
using HotelBooking.Api.DTOs.Bookings;
using HotelBooking.Api.DTOs.Rooms;
using HotelBooking.Api.Models;

namespace HotelBooking.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomResponse>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src =>
                    src.Amenities.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)));

            CreateMap<Booking, BookingResponse>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Room.RoomType));
        }
    }
}
