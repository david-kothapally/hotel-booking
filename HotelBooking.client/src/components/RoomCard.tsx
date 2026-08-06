import { Link } from "react-router-dom";
import type { Room } from "../interfaces/Room";
import RoomAmenities from "./RoomAmenities";

interface RoomCardProps {
  room: Room;
  checkIn?: string;
  checkOut?: string;
  guests?: number;
}

function RoomCard({ room, checkIn, checkOut, guests }: RoomCardProps) {
  const detailsQuery =
    checkIn && checkOut && guests
      ? `?${new URLSearchParams({ checkIn, checkOut, guests: String(guests) }).toString()}`
      : "";

  return (
    <div className="overflow-hidden rounded-md border border-gray-200 bg-white">
      <Link to={`/rooms/${room.roomId}${detailsQuery}`}>
        <img
          src={room.imageUrl}
          alt={room.roomType}
          className="h-48 w-full object-cover"
        />
      </Link>

      <div className="p-4">
        <h3 className="text-lg font-serif text-brand-primary">{room.roomType}</h3>

        <p className="mt-1 text-sm text-brand-muted">
          Max {room.maxGuests} guests
        </p>

        <div className="mt-2">
          <RoomAmenities amenities={room.amenities} />
        </div>

        <div className="mt-4 flex items-center justify-between">
          <span className="font-semibold text-brand-ink">
            ${room.pricePerNight.toFixed(2)}{" "}
            <span className="text-xs text-brand-muted">/ night</span>
          </span>

          <Link
            to={`/rooms/${room.roomId}${detailsQuery}`}
            className="rounded bg-brand-accent px-4 py-2 text-sm font-semibold text-white"
          >
            View Details
          </Link>
        </div>
      </div>
    </div>
  );
}

export default RoomCard;
