import React, { useState, useEffect } from "react";
import InputField from "../inputs/InputField";
import Button from "../common/button/Button";
import Checkbox from "../inputs/Checkbox";
import { validateLoginForm } from "../../validation/LoginValidation";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import './LoginForm.css';

export default function LoginForm() {
  // const { id } = useParams();
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    rememberMe: false,
  });

  const [errors, setErrors] = useState({});
  const [serverError, setServerError] = useState("");
  const navigate = useNavigate();


  useEffect(() => {
    const savedEmail = localStorage.getItem('email');
    const savedPassword = localStorage.getItem('password');

    if (savedEmail) {
      setFormData((prev) => ({
        ...prev,
        email: savedEmail,
        password: savedPassword,
        rememberMe: true,
      }));
    }
  }, []);


  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const validationErrors = validateLoginForm(formData);
    if (validationErrors) {
      setErrors(validationErrors);
      return;
    }

    try {
      setErrors({});
      setServerError("");

      const response = await axios.post("http://localhost:5077/api/Auth/login", formData, {
        headers: {
            'Content-Type': 'application/json',
        },
        withCredentials: true,
      });
      
      console.log("Успешный вход:", response.data);
      localStorage.setItem('token', response.data.token);

      if (formData.rememberMe) {
        localStorage.setItem('email', formData.email);
        localStorage.setItem('password', formData.password);
      } else {
        localStorage.removeItem('email');
        localStorage.removeItem('password');
      }

      navigate(`/`);

    } catch (error) {
      setServerError("Ошибка входа. Пожалуйста, проверьте данные.");
    }
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <h2 className="login-header">Вход</h2>

        <form onSubmit={handleSubmit}>
          {serverError && <div className="server-error">{serverError}</div>}

          <InputField
            label="Email"
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            error={errors.email}
          />
          <InputField
            label="Пароль"
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            error={errors.password}
          />
          <Checkbox
            label="Запомнить меня"
            name="rememberMe"
            checked={formData.rememberMe}
            onChange={handleChange}
          />
          <Button label="Войти" buttonType="submit" />
        </form>
        <div className="register-redirect">
          Нет аккаунта? <Link to="/register">Регистрация</Link>
        </div>
      </div>
    </div>
  );
}

