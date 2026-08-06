interface BookingSummaryProps {
  roomType: string;
  checkInDate: string;
  checkOutDate: string;
  guestCount: number;
  nights: number;
  totalPrice: number;
}

function BookingSummary({
  roomType,
  checkInDate,
  checkOutDate,
  guestCount,
  nights,
  totalPrice,
}: BookingSummaryProps) {
  return (
    <div className="rounded-md border border-gray-200 bg-white p-6">
      <h2 className="text-lg font-serif text-brand-primary">Booking Summary</h2>

      <dl className="mt-4 space-y-2 text-sm text-brand-ink">
        <div className="flex justify-between">
          <dt className="text-brand-muted">Room</dt>
          <dd>{roomType}</dd>
        </div>
        <div className="flex justify-between">
          <dt className="text-brand-muted">Check In</dt>
          <dd>{checkInDate}</dd>
        </div>
        <div className="flex justify-between">
          <dt className="text-brand-muted">Check Out</dt>
          <dd>{checkOutDate}</dd>
        </div>
        <div className="flex justify-between">
          <dt className="text-brand-muted">Guests</dt>
          <dd>{guestCount}</dd>
        </div>
        <div className="flex justify-between font-semibold">
          <dt>Total ({nights} nights)</dt>
          <dd>${totalPrice.toFixed(2)}</dd>
        </div>
      </dl>
    </div>
  );
}

export default BookingSummary;
