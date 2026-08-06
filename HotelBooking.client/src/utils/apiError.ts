import { isAxiosError } from "axios";

export function getApiErrorMessage(err: unknown, fallback: string): string {
  if (isAxiosError(err) && err.response?.data) {
    const data = err.response.data as { detail?: string; errors?: Record<string, string[]> };

    if (data.errors) {
      return Object.values(data.errors).flat().join(" ");
    }

    if (data.detail) {
      return data.detail;
    }
  }

  return fallback;
}
