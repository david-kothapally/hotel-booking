using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Amenities", "Description", "ImageUrl", "IsActive", "MaxGuests", "PricePerNight", "RoomNumber", "RoomType" },
                values: new object[,]
                {
                    { 1, "WiFi,TV,Air Conditioning", "A comfortable standard room with a king bed, perfect for solo travelers or couples.", "https://picsum.photos/seed/room101/800/600", true, 2, 129.00m, "101", "Standard King" },
                    { 2, "WiFi,TV,Air Conditioning", "A cozy standard room with a queen bed and modern amenities.", "https://picsum.photos/seed/room102/800/600", true, 2, 119.00m, "102", "Standard Queen" },
                    { 3, "WiFi,TV,Air Conditioning,Mini Fridge", "A standard room with two double beds, ideal for small groups.", "https://picsum.photos/seed/room103/800/600", true, 3, 139.00m, "103", "Standard Double" },
                    { 4, "WiFi,TV,Minibar,Air Conditioning,City View", "A spacious deluxe room with a king bed and city views.", "https://picsum.photos/seed/room201/800/600", true, 3, 189.00m, "201", "Deluxe King" },
                    { 5, "WiFi,TV,Minibar,Air Conditioning,City View,Sofa Bed", "A deluxe suite with a separate seating area and a sofa bed.", "https://picsum.photos/seed/room202/800/600", true, 4, 219.00m, "202", "Deluxe Suite" },
                    { 6, "WiFi,TV,Minibar,Air Conditioning", "A deluxe room with two queen beds, great for families.", "https://picsum.photos/seed/room203/800/600", true, 4, 199.00m, "203", "Deluxe Double" },
                    { 7, "WiFi,TV,Minibar,Air Conditioning,City View,Work Desk,Bathrobe", "An executive suite with a private work desk and premium comforts.", "https://picsum.photos/seed/room301/800/600", true, 4, 289.00m, "301", "Executive Suite" },
                    { 8, "WiFi,TV,Minibar,Air Conditioning,Balcony", "A junior suite with a private balcony overlooking the city.", "https://picsum.photos/seed/room302/800/600", true, 3, 259.00m, "302", "Junior Suite" },
                    { 9, "WiFi,TV,Minibar,Air Conditioning,City View,Jacuzzi,Private Balcony,Butler Service", "The ultimate luxury suite with a private jacuzzi and butler service.", "https://picsum.photos/seed/room401/800/600", true, 6, 599.00m, "401", "Presidential Suite" },
                    { 10, "WiFi,TV,Minibar,Air Conditioning,Two Bedrooms,Kitchenette", "A spacious two-bedroom suite with a kitchenette, perfect for families.", "https://picsum.photos/seed/room402/800/600", true, 6, 349.00m, "402", "Family Suite" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 10);
        }
    }
}
