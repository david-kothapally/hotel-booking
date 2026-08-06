export interface Room {
  roomId: number;
  roomNumber: string;
  roomType: string;
  description: string;
  pricePerNight: number;
  maxGuests: number;
  amenities: string[];
  imageUrl: string;
}
