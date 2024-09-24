import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import LoginForm from "./components/login/LoginForm";
import RegistrationForm from "./components/registration/RegistrationForm";
import AdForm from "./components/baraholka/adForm/AdForm";
import TicketForm from "./components/ticket/ticketForm/ticketForm";
import AdPage from "./components/baraholka/adPage/AdPage";
import AdsPage from "./components/baraholka/mainPage/AdsPage";
import NavBar from "./components/common/navigation bar/NavBar";

function App() {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegistrationForm />} />
        <Route path="/adform" element={<AdForm />} />
        <Route path="/ticket" element={<TicketForm />}/>
        <Route path="/ticket/:ticketId" element={<TicketForm />}/>
        <Route path="/ticket/:tickets" element={<TicketForm />}/>
        <Route path="/ads/edit/:adId" element={<AdForm />} />
        <Route path="/ads/:adId" element={<AdPage />} />
        <Route path="/ads" element={<AdsPage />} />
      </Routes>
    </Router>
  );
}

export default App;
