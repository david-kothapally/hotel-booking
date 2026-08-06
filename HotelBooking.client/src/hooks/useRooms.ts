import { useState } from "react";
import type { Room } from "../interfaces/Room";
import type { SearchRequest } from "../interfaces/SearchRequest";
import { getAvailableRooms, getRoomById } from "../api/roomService";
import { getApiErrorMessage } from "../utils/apiError";

export function useRooms() {
  const [rooms, setRooms] = useState<Room[]>([]);
  const [room, setRoom] = useState<Room | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [hasSearched, setHasSearched] = useState(false);
  const [lastSearch, setLastSearch] = useState<SearchRequest | null>(null);

  async function searchRooms(searchRequest: SearchRequest) {
    setIsLoading(true);
    setError(null);
    setHasSearched(true);
    setLastSearch(searchRequest);

    try {
      const result = await getAvailableRooms(searchRequest);
      setRooms(result);
    } catch (err) {
      setError(getApiErrorMessage(err, "Unable to load available rooms. Please try again."));
    } finally {
      setIsLoading(false);
    }
  }

  function resetSearch() {
    setRooms([]);
    setHasSearched(false);
    setLastSearch(null);
    setError(null);
  }

  async function fetchRoomById(id: number) {
    setIsLoading(true);
    setError(null);

    try {
      const result = await getRoomById(id);
      setRoom(result);
    } catch (err) {
      setError(getApiErrorMessage(err, "Unable to load room details. Please try again."));
    } finally {
      setIsLoading(false);
    }
  }

  return { rooms, room, isLoading, error, hasSearched, lastSearch, searchRooms, resetSearch, fetchRoomById };
}
