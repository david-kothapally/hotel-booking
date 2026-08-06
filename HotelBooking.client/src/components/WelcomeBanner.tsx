function WelcomeBanner() {
  return (
    <div className="relative overflow-hidden rounded-md">
      <img
        src="https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=1600&q=80"
        alt="Hotel pool and lounge area"
        className="h-80 w-full object-cover"
      />
      <div className="absolute inset-0 flex flex-col items-center justify-center bg-black/40 px-6 text-center">
        <h1 className="font-serif text-4xl italic text-white sm:text-5xl">
          Welcome to Grand Vista Hotel
        </h1>
        <p className="mt-3 max-w-md text-base text-white/90 sm:text-lg">
          Discover comfort and elegance for your next stay. Search available rooms and book your escape today.
        </p>
      </div>
    </div>
  );
}

export default WelcomeBanner;
