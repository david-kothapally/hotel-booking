import { useEffect, useState, type SubmitEvent } from "react";
import { useParams, useSearchParams, useNavigate } from "react-router-dom";
import BookingSummary from "../components/BookingSummary";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { useRooms } from "../hooks/useRooms";
import { useBooking } from "../hooks/useBooking";
import { calculateNights } from "../utils/dateHelpers";
import { isRequired, isValidEmail } from "../utils/validation";

function BookingPage() {
  const { roomId } = useParams();
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const { room, isLoading, error, fetchRoomById } = useRooms();
  const { isSubmitting, error: bookingError, submitBooking } = useBooking();

  const [guestName, setGuestName] = useState("");
  const [email, setEmail] = useState("");
  const [specialRequests, setSpecialRequests] = useState("");
  const [validationError, setValidationError] = useState<string | null>(null);

  const checkInDate = searchParams.get("checkIn");
  const checkOutDate = searchParams.get("checkOut");
  const guestCount = searchParams.get("guests");

  useEffect(() => {
    if (roomId) {
      fetchRoomById(Number(roomId));
    }
  }, [roomId]);

  if (isLoading) {
    return <LoadingSpinner message="Loading booking details..." />;
  }

  if (error) {
    return <ErrorMessage message={error} />;
  }

  if (!room) {
    return null;
  }

  if (!checkInDate || !checkOutDate || !guestCount) {
    return <ErrorMessage message="Missing booking dates. Please search for a room first." />;
  }

  const nights = calculateNights(checkInDate, checkOutDate);
  const totalPrice = nights * room.pricePerNight;

  async function handleSubmit(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!isRequired(guestName)) {
      setValidationError("Guest name is required.");
      return;
    }

    if (!isValidEmail(email)) {
      setValidationError("Please enter a valid email address.");
      return;
    }

    setValidationError(null);

    const booking = await submitBooking({
      roomId: Number(roomId),
      checkInDate: checkInDate!,
      checkOutDate: checkOutDate!,
      guestCount: Number(guestCount),
      guestName,
      email,
      specialRequests: specialRequests || undefined,
    });

    if (booking) {
      navigate(`/confirmation/${booking.bookingReference}`, { replace: true });
    }
  }

  return (
    <div className="px-6 py-8">
      <div className="grid gap-6 md:grid-cols-2">
        <BookingSummary
          roomType={room.roomType}
          checkInDate={checkInDate}
          checkOutDate={checkOutDate}
          guestCount={Number(guestCount)}
          nights={nights}
          totalPrice={totalPrice}
        />

        <form
          onSubmit={handleSubmit}
          className="rounded-md border border-gray-200 bg-white p-6"
        >
          <h2 className="text-lg font-serif text-brand-primary">Guest Details</h2>

          {validationError && (
            <div className="mt-4">
              <ErrorMessage message={validationError} />
            </div>
          )}

          {bookingError && (
            <div className="mt-4">
              <ErrorMessage message={bookingError} />
            </div>
          )}

          <div className="mt-4 flex flex-col">
            <label htmlFor="guestName" className="text-xs uppercase tracking-widest text-brand-muted">
              Guest Name
            </label>
            <input
              id="guestName"
              type="text"
              value={guestName}
              onChange={(event) => setGuestName(event.target.value)}
              className="mt-1 rounded border border-gray-300 px-2 py-1 text-brand-ink"
            />
          </div>

          <div className="mt-4 flex flex-col">
            <label htmlFor="email" className="text-xs uppercase tracking-widest text-brand-muted">
              Email
            </label>
            <input
              id="email"
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              className="mt-1 rounded border border-gray-300 px-2 py-1 text-brand-ink"
            />
          </div>

          <div className="mt-4 flex flex-col">
            <label htmlFor="specialRequests" className="text-xs uppercase tracking-widest text-brand-muted">
              Special Requests
            </label>
            <textarea
              id="specialRequests"
              value={specialRequests}
              onChange={(event) => setSpecialRequests(event.target.value)}
              rows={3}
              className="mt-1 rounded border border-gray-300 px-2 py-1 text-brand-ink"
            />
          </div>

          <button
            type="submit"
            disabled={isSubmitting}
            className="mt-6 w-full rounded bg-brand-accent px-6 py-2 text-sm font-semibold text-white disabled:opacity-60"
          >
            {isSubmitting ? "Booking..." : "Confirm Booking"}
          </button>
        </form>
      </div>
    </div>
  );
}

export default BookingPage;
