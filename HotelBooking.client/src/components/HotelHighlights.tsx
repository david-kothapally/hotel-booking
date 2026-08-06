const highlights = [
  {
    imageUrl: "https://images.unsplash.com/photo-1445019980597-93fa8acb246c?auto=format&fit=crop&w=1600&q=80",
    title: "Elegant Rooms & Suites",
    description: "Thoughtfully designed accommodations with comfort and style for every stay.",
  },
  {
    imageUrl: "https://images.unsplash.com/photo-1559339352-11d035aa65de?auto=format&fit=crop&w=900&q=80",
    title: "Fine Dining",
    description: "Enjoy a curated selection of on-site restaurants and lounges.",
  },
  {
    imageUrl: "https://images.unsplash.com/photo-1540541338287-41700207dee6?auto=format&fit=crop&w=900&q=80",
    title: "Pool & Spa",
    description: "Unwind and recharge with our resort-style pool and spa amenities.",
  },
];

function HotelHighlights() {
  return (
    <div className="grid grid-cols-1 gap-8 sm:grid-cols-3">
      {highlights.map((highlight) => (
        <div key={highlight.title} className="text-center">
          <img
            src={highlight.imageUrl}
            alt={highlight.title}
            className="h-48 w-full rounded-md object-cover"
          />
          <h3 className="mt-4 font-serif text-xl text-brand-primary">{highlight.title}</h3>
          <p className="mt-2 text-sm text-brand-muted">{highlight.description}</p>
        </div>
      ))}
    </div>
  );
}

export default HotelHighlights;
