import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import LoginForm from "./components/login/LoginForm";
import RegistrationForm from "./components/registration/RegistrationForm";
import AdForm from "./components/baraholka/adForm/AdForm";
import AdPage from "./components/baraholka/adPage/AdPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegistrationForm />} />
        <Route path="/adform" element={<AdForm />} />
      </Routes>
    </Router>
  );
}

export default App;
