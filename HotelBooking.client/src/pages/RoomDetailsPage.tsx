import { useEffect } from "react";
import { useParams, useSearchParams, Link } from "react-router-dom";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import RoomAmenities from "../components/RoomAmenities";
import { useRooms } from "../hooks/useRooms";

function RoomDetailsPage() {
  const { id } = useParams();
  const [searchParams] = useSearchParams();
  const { room, isLoading, error, fetchRoomById } = useRooms();

  const checkIn = searchParams.get("checkIn");
  const checkOut = searchParams.get("checkOut");
  const guests = searchParams.get("guests");

  useEffect(() => {
    if (id) {
      fetchRoomById(Number(id));
    }
  }, [id]);

  if (isLoading) {
    return <LoadingSpinner message="Loading room details..." />;
  }

  if (error) {
    return <ErrorMessage message={error} />;
  }

  if (!room) {
    return null;
  }

  const bookingQuery =
    checkIn && checkOut && guests
      ? `?${new URLSearchParams({ checkIn, checkOut, guests }).toString()}`
      : "";

  return (
    <div className="px-6 py-8">
      <div className="mx-auto max-w-3xl overflow-hidden rounded-md border border-gray-200 bg-white">
        <img
          src={room.imageUrl}
          alt={room.roomType}
          className="h-72 w-full object-cover"
        />

        <div className="p-6">
          <h2 className="text-2xl font-serif text-brand-primary">{room.roomType}</h2>
          <p className="mt-2 text-sm text-brand-ink">{room.description}</p>

          <div className="mt-4">
            <RoomAmenities amenities={room.amenities} />
          </div>

          <p className="mt-4 text-sm text-brand-muted">Max {room.maxGuests} guests</p>

          {checkIn && checkOut && guests && (
            <div className="mt-4 space-y-1 text-sm text-brand-ink">
              <p><span className="text-brand-muted">Check In:</span> {checkIn}</p>
              <p><span className="text-brand-muted">Check Out:</span> {checkOut}</p>
              <p><span className="text-brand-muted">Guests:</span> {guests}</p>
            </div>
          )}

          <div className="mt-6 flex items-center justify-between">
            <span className="text-lg font-semibold text-brand-ink">
              ${room.pricePerNight.toFixed(2)}{" "}
              <span className="text-xs text-brand-muted">/ night</span>
            </span>

            <Link
              to={`/booking/${room.roomId}${bookingQuery}`}
              className="rounded bg-brand-accent px-6 py-2 text-sm font-semibold text-white"
            >
              Book Now
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RoomDetailsPage;
