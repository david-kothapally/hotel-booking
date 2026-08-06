import { Link } from "react-router-dom";

function Header() {
  return (
    <header className="bg-brand-primary px-6 py-4">
      <Link to="/" className="inline-block">
        <span className="block font-serif italic text-2xl text-white">
          Grand Vista Hotel
        </span>
        <span className="block text-xs tracking-widest text-white/70 uppercase">
          Las Vegas
        </span>
      </Link>
    </header>
  );
}

export default Header;
