import axiosClient from "./axiosClient";
import type { Room } from "../interfaces/Room";
import type { SearchRequest } from "../interfaces/SearchRequest";

export async function getAvailableRooms(searchRequest: SearchRequest): Promise<Room[]> {
  const response = await axiosClient.get<Room[]>("/api/rooms/search", {
    params: searchRequest,
  });

  return response.data;
}

export async function getRoomById(id: number): Promise<Room> {
  const response = await axiosClient.get<Room>(`/api/rooms/${id}`);

  return response.data;
}
