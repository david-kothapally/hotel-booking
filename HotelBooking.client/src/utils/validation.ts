
export function isValidDateRange(checkIn: string, checkOut: string): boolean {
  if (!checkIn || !checkOut) {
    return false;
  }

  return new Date(checkOut).getTime() > new Date(checkIn).getTime();
}

export function isValidEmail(email: string): boolean {
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
}

export function isRequired(value: string): boolean {
  return value.trim().length > 0;
}
