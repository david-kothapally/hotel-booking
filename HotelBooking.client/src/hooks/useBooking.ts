import { useState } from "react";
import type { BookingRequest } from "../interfaces/BookingRequest";
import type { BookingResponse } from "../interfaces/BookingResponse";
import { createBooking, getBookingByReference } from "../api/bookingService";
import { getApiErrorMessage } from "../utils/apiError";

export function useBooking() {
  const [booking, setBooking] = useState<BookingResponse | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function submitBooking(bookingRequest: BookingRequest): Promise<BookingResponse | null> {
    setIsSubmitting(true);
    setError(null);

    try {
      const result = await createBooking(bookingRequest);
      setBooking(result);
      return result;
    } catch (err) {
      setError(getApiErrorMessage(err, "Unable to complete your booking. Please try again."));
      return null;
    } finally {
      setIsSubmitting(false);
    }
  }

  async function fetchBookingByReference(reference: string) {
    setIsLoading(true);
    setError(null);

    try {
      const result = await getBookingByReference(reference);
      setBooking(result);
    } catch (err) {
      setError(getApiErrorMessage(err, "Unable to load your booking. Please try again."));
    } finally {
      setIsLoading(false);
    }
  }

  return { booking, isSubmitting, isLoading, error, submitBooking, fetchBookingByReference };
}
