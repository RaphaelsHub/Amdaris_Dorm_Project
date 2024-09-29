import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import LoginForm from "./components/login/LoginForm";
import RegistrationForm from "./components/registration/RegistrationForm";
import AdForm from "./components/baraholka/adForm/AdForm";
import TicketForm from "./components/ticket/ticketForm/ticketForm";
import AdPage from "./components/baraholka/adPage/AdPage";
import AdsPage from "./components/baraholka/mainPage/AdsPage";
import NavBar from "./components/common/navigation bar/NavBar";
import TicketsPage from "./components/ticket/ticketsPage/ticketsPage";
import TicketPage from "./components/ticket/ticketPage/ticketPage";

function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegistrationForm />} />
        <Route path="/adform" element={<AdForm />} />
        <Route path="/ticket" element={<TicketForm />}/>
        <Route path="/tickets/:ticketId" element={<TicketPage />}/>
        <Route path="/tickets/edit/:ticketId" element={<TicketForm />}/>
        <Route path="/tickets" element={<TicketsPage />}/>
        <Route path="/ads/edit/:adId" element={<AdForm />} />
        <Route path="/ads/:adId" element={<AdPage />} />
        <Route path="/ads" element={<AdsPage />} />
      </Routes>
    </Router>
  );
}

export default App;
