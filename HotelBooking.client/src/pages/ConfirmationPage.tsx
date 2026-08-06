import { useEffect } from "react";
import { useParams } from "react-router-dom";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import { useBooking } from "../hooks/useBooking";

function ConfirmationPage() {
  const { reference } = useParams();
  const { booking, isLoading, error, fetchBookingByReference } = useBooking();

  useEffect(() => {
    if (reference) {
      fetchBookingByReference(reference);
    }
  }, [reference]);

  if (isLoading) {
    return <LoadingSpinner message="Loading your confirmation..." />;
  }

  if (error) {
    return <ErrorMessage message={error} />;
  }

  if (!booking) {
    return null;
  }

  return (
    <div className="px-6 py-8">
      <div className="mx-auto max-w-xl rounded-md border border-gray-200 bg-white p-6">
        <h2 className="text-lg font-serif text-brand-primary">Booking Confirmed</h2>
        <p className="mt-1 text-sm text-brand-muted">Reference: {booking.bookingReference}</p>

        <dl className="mt-6 space-y-2 text-sm text-brand-ink">
          <div className="flex justify-between">
            <dt className="text-brand-muted">Room</dt>
            <dd>{booking.roomType} (Room {booking.roomNumber})</dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-brand-muted">Check In</dt>
            <dd>{booking.checkInDate}</dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-brand-muted">Check Out</dt>
            <dd>{booking.checkOutDate}</dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-brand-muted">Guests</dt>
            <dd>{booking.guestCount}</dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-brand-muted">Guest Name</dt>
            <dd>{booking.guestName}</dd>
          </div>
          <div className="flex justify-between">
            <dt className="text-brand-muted">Email</dt>
            <dd>{booking.email}</dd>
          </div>
          <div className="flex justify-between font-semibold">
            <dt>Total Price</dt>
            <dd>${booking.totalPrice.toFixed(2)}</dd>
          </div>
        </dl>
      </div>
    </div>
  );
}

export default ConfirmationPage;
