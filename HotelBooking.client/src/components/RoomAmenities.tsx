interface RoomAmenitiesProps {
  amenities: string[];
}

function RoomAmenities({ amenities }: RoomAmenitiesProps) {
  return (
    <ul className="flex flex-wrap gap-2 text-xs text-brand-ink">
      {amenities.map((amenity) => (
        <li key={amenity} className="rounded-full bg-gray-100 px-2 py-1">
          {amenity}
        </li>
      ))}
    </ul>
  );
}

export default RoomAmenities;
