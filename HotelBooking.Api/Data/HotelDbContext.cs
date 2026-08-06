using HotelBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // A Room has many Bookings. If a Room is ever deleted, its Bookings
            // must not be deleted along with it, so history is preserved.
            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Room)
                .WithMany(room => room.Bookings)
                .HasForeignKey(booking => booking.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // BookingReference is the public-facing identifier (e.g. HTL-20260805-3482)
            // and must be unique so it can be used to look up a booking safely.
            modelBuilder.Entity<Booking>()
                .HasIndex(booking => booking.BookingReference)
                .IsUnique();

            // RoomNumber is the natural business key for a room and must be unique.
            modelBuilder.Entity<Room>()
                .HasIndex(room => room.RoomNumber)
                .IsUnique();

            // Seeded via HasData so the rows are baked into a migration
            // (dotnet ef migrations add) instead of being inserted at app startup.
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, RoomNumber = "101", RoomType = "Standard King", Description = "A comfortable standard room with a king bed, perfect for solo travelers or couples.", PricePerNight = 129.00m, MaxGuests = 2, Amenities = "WiFi,TV,Air Conditioning", ImageUrl = "https://picsum.photos/seed/room101/800/600", IsActive = true },
                new Room { RoomId = 2, RoomNumber = "102", RoomType = "Standard Queen", Description = "A cozy standard room with a queen bed and modern amenities.", PricePerNight = 119.00m, MaxGuests = 2, Amenities = "WiFi,TV,Air Conditioning", ImageUrl = "https://picsum.photos/seed/room102/800/600", IsActive = true },
                new Room { RoomId = 3, RoomNumber = "103", RoomType = "Standard Double", Description = "A standard room with two double beds, ideal for small groups.", PricePerNight = 139.00m, MaxGuests = 3, Amenities = "WiFi,TV,Air Conditioning,Mini Fridge", ImageUrl = "https://picsum.photos/seed/room103/800/600", IsActive = true },
                new Room { RoomId = 4, RoomNumber = "201", RoomType = "Deluxe King", Description = "A spacious deluxe room with a king bed and city views.", PricePerNight = 189.00m, MaxGuests = 3, Amenities = "WiFi,TV,Minibar,Air Conditioning,City View", ImageUrl = "https://picsum.photos/seed/room201/800/600", IsActive = true },
                new Room { RoomId = 5, RoomNumber = "202", RoomType = "Deluxe Suite", Description = "A deluxe suite with a separate seating area and a sofa bed.", PricePerNight = 219.00m, MaxGuests = 4, Amenities = "WiFi,TV,Minibar,Air Conditioning,City View,Sofa Bed", ImageUrl = "https://picsum.photos/seed/room202/800/600", IsActive = true },
                new Room { RoomId = 6, RoomNumber = "203", RoomType = "Deluxe Double", Description = "A deluxe room with two queen beds, great for families.", PricePerNight = 199.00m, MaxGuests = 4, Amenities = "WiFi,TV,Minibar,Air Conditioning", ImageUrl = "https://picsum.photos/seed/room203/800/600", IsActive = true },
                new Room { RoomId = 7, RoomNumber = "301", RoomType = "Executive Suite", Description = "An executive suite with a private work desk and premium comforts.", PricePerNight = 289.00m, MaxGuests = 4, Amenities = "WiFi,TV,Minibar,Air Conditioning,City View,Work Desk,Bathrobe", ImageUrl = "https://picsum.photos/seed/room301/800/600", IsActive = true },
                new Room { RoomId = 8, RoomNumber = "302", RoomType = "Junior Suite", Description = "A junior suite with a private balcony overlooking the city.", PricePerNight = 259.00m, MaxGuests = 3, Amenities = "WiFi,TV,Minibar,Air Conditioning,Balcony", ImageUrl = "https://picsum.photos/seed/room302/800/600", IsActive = true },
                new Room { RoomId = 9, RoomNumber = "401", RoomType = "Presidential Suite", Description = "The ultimate luxury suite with a private jacuzzi and butler service.", PricePerNight = 599.00m, MaxGuests = 6, Amenities = "WiFi,TV,Minibar,Air Conditioning,City View,Jacuzzi,Private Balcony,Butler Service", ImageUrl = "https://picsum.photos/seed/room401/800/600", IsActive = true },
                new Room { RoomId = 10, RoomNumber = "402", RoomType = "Family Suite", Description = "A spacious two-bedroom suite with a kitchenette, perfect for families.", PricePerNight = 349.00m, MaxGuests = 6, Amenities = "WiFi,TV,Minibar,Air Conditioning,Two Bedrooms,Kitchenette", ImageUrl = "https://picsum.photos/seed/room402/800/600", IsActive = true }
            );
        }
    }
}
