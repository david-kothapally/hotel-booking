import { Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import SearchPage from "./pages/SearchPage";
import RoomDetailsPage from "./pages/RoomDetailsPage";
import BookingPage from "./pages/BookingPage";
import ConfirmationPage from "./pages/ConfirmationPage";

function App() {
  return (
    <>
      <Header />
      <Routes>
        <Route path="/" element={<SearchPage />} />
        <Route path="/rooms/:id" element={<RoomDetailsPage />} />
        <Route path="/booking/:roomId" element={<BookingPage />} />
        <Route path="/confirmation/:reference" element={<ConfirmationPage />} />
      </Routes>
    </>
  );
}

export default App;