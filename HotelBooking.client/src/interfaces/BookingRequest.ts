export interface BookingRequest {
    roomId: number;
    checkInDate: string;
    checkOutDate: string;
    guestCount: number;
    guestName: string;
    email: string;
    specialRequests?: string;
}
