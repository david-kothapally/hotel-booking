import { useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import SearchBar from "../components/SearchBar";
import RoomCard from "../components/RoomCard";
import LoadingSpinner from "../components/LoadingSpinner";
import ErrorMessage from "../components/ErrorMessage";
import WelcomeBanner from "../components/WelcomeBanner";
import HotelHighlights from "../components/HotelHighlights";
import { useRooms } from "../hooks/useRooms";

function SearchPage() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { rooms, isLoading, error, hasSearched, lastSearch, searchRooms, resetSearch } = useRooms();

  const checkIn = searchParams.get("checkIn");
  const checkOut = searchParams.get("checkOut");
  const guests = searchParams.get("guests");

  useEffect(() => {
    if (checkIn && checkOut && guests) {
      searchRooms({ checkIn, checkOut, guests: Number(guests) });
    } else {
      resetSearch();
    }
  }, [checkIn, checkOut, guests]);

  function handleSearch(params: { checkIn: string; checkOut: string; guestCount: number }) {
    setSearchParams({
      checkIn: params.checkIn,
      checkOut: params.checkOut,
      guests: String(params.guestCount),
    });
  }

  return (
    <div className="px-6 py-8">
      <div className="mx-auto max-w-3xl">
        <SearchBar
          key={searchParams.toString()}
          onSearch={handleSearch}
          initialCheckIn={checkIn ?? undefined}
          initialCheckOut={checkOut ?? undefined}
          initialGuestCount={guests ? Number(guests) : undefined}
        />
      </div>

      {!isLoading && !error && !hasSearched && (
        <div className="mt-8 space-y-10">
          <WelcomeBanner />
          <HotelHighlights />
        </div>
      )}

      <div className="mt-6">
        {isLoading && <LoadingSpinner message="Searching rooms..." />}

        {!isLoading && error && <ErrorMessage message={error} />}

        {!isLoading && !error && hasSearched && rooms.length === 0 && (
          <p className="pt-10 text-center text-brand-muted">No rooms found for your search criteria.</p>
        )}

        {!isLoading && !error && rooms.length > 0 && (
          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3">
            {rooms.map((room) => (
              <RoomCard
                key={room.roomId}
                room={room}
                checkIn={lastSearch?.checkIn}
                checkOut={lastSearch?.checkOut}
                guests={lastSearch?.guests}
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default SearchPage;
