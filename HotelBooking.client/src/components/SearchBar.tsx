import { useState, type SubmitEvent } from "react";
import ErrorMessage from "./ErrorMessage";
import { isValidDateRange } from "../utils/validation";

interface SearchBarProps {
  onSearch: (params: { checkIn: string; checkOut: string; guestCount: number }) => void;
  initialCheckIn?: string;
  initialCheckOut?: string;
  initialGuestCount?: number;
}

function SearchBar({ onSearch, initialCheckIn, initialCheckOut, initialGuestCount }: SearchBarProps) {
  const [checkIn, setCheckIn] = useState(initialCheckIn ?? "");
  const [checkOut, setCheckOut] = useState(initialCheckOut ?? "");
  const [guestCount, setGuestCount] = useState(initialGuestCount ?? 1);
  const [validationError, setValidationError] = useState<string | null>(null);

  const today = new Date().toISOString().split("T")[0];
  const maxDate = new Date();
  maxDate.setFullYear(maxDate.getFullYear() + 2);
  const maxDateString = maxDate.toISOString().split("T")[0];

  function handleSubmit(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!isValidDateRange(checkIn, checkOut)) {
      setValidationError("Please select a check-out date after the check-in date.");
      return;
    }

    setValidationError(null);
    onSearch({ checkIn, checkOut, guestCount });
  }

  return (
    <div>
      {validationError && (
        <div className="mb-4">
          <ErrorMessage message={validationError} />
        </div>
      )}

      <form
        onSubmit={handleSubmit}
        className="flex flex-wrap items-end gap-4 rounded-md border border-gray-200 bg-white p-4"
      >
        <div className="flex flex-col">
          <label htmlFor="checkIn" className="text-xs uppercase tracking-widest text-brand-muted">
            Check In
          </label>
          <input
            id="checkIn"
            type="date"
            value={checkIn}
            min={today}
            max={maxDateString}
            onChange={(event) => setCheckIn(event.target.value)}
            className="mt-1 rounded border border-gray-300 px-2 py-1 text-brand-ink"
          />
        </div>

        <div className="flex flex-col">
          <label htmlFor="checkOut" className="text-xs uppercase tracking-widest text-brand-muted">
            Check Out
          </label>
          <input
            id="checkOut"
            type="date"
            value={checkOut}
            min={checkIn || today}
            max={maxDateString}
            onChange={(event) => setCheckOut(event.target.value)}
            className="mt-1 rounded border border-gray-300 px-2 py-1 text-brand-ink"
          />
        </div>

        <div className="flex flex-col">
          <label htmlFor="guestCount" className="text-xs uppercase tracking-widest text-brand-muted">
            Guests
          </label>
          <input
            id="guestCount"
            type="number"
            min={1}
            value={guestCount}
            onChange={(event) => setGuestCount(Number(event.target.value))}
            className="mt-1 w-20 rounded border border-gray-300 px-2 py-1 text-brand-ink"
          />
        </div>

        <button
          type="submit"
          className="rounded bg-brand-accent px-6 py-2 font-semibold text-white"
        >
          Search
        </button>
      </form>
    </div>
  );
}

export default SearchBar;
