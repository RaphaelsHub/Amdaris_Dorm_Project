import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import LoginForm from "./components/login/LoginForm";
import RegistrationForm from "./components/registration/RegistrationForm";
import AdForm from "./components/baraholka/adForm/AdForm";
import TicketForm from "./components/ticket/ticketForm/ticketForm";
import AdPage from "./components/baraholka/adPage/AdPage";
import AdsPage from "./components/baraholka/mainPage/AdsPage";
import NavBar from "./components/common/navigation bar/NavBar";
import Chat from "./components/chat/Chat";

function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegistrationForm />} />
        <Route path="/adform" element={<AdForm />} />
        <Route path="/ticket" element={<TicketForm userId={2}/>} />//временно, пока нет аккаунта и не придумала как прокидывать айдишник пользователя между страницами
        <Route path="/ads/edit/:adId" element={<AdForm />} />
        <Route path="/ads/:adId" element={<AdPage />} />
        <Route path="/ads" element={<AdsPage />} />
        <Route path="/chat" element={<Chat />} />
      </Routes>
    </Router>
  );
}

export default App;
