import type { BookingStatus } from "../types/BookingStatus";

export interface BookingResponse {
    bookingReference: string;
    roomId: number;
    roomNumber: string;
    roomType: string;
    checkInDate: string;
    checkOutDate: string;
    guestCount: number;
    guestName: string;
    email: string;
    specialRequests?: string;
    totalPrice: number;
    status: BookingStatus;
    createdDate: string;
}
