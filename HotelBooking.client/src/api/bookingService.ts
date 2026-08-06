import axiosClient from "./axiosClient";
import type { BookingRequest } from "../interfaces/BookingRequest";
import type { BookingResponse } from "../interfaces/BookingResponse";

export async function createBooking(bookingRequest: BookingRequest): Promise<BookingResponse> {
  const response = await axiosClient.post<BookingResponse>("/api/bookings", bookingRequest);

  return response.data;
}

export async function getBookingByReference(reference: string): Promise<BookingResponse> {
  const response = await axiosClient.get<BookingResponse>(`/api/bookings/${reference}`);

  return response.data;
}
