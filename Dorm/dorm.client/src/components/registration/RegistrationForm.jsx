import React, { useState } from "react";
import InputField from "../inputs/InputField";
import Button from "../common/button/Button";
import { validateForm } from "../../validation/FormValidation";
import axios from "axios";
import './RegistrationForm.css'
import { useNavigate } from "react-router-dom";

export default function RegistrationForm() {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    firstName: "",
    lastName: "",
    group: ""
  });

  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [message, setMessage] = useState(null);
  const navigate = useNavigate();


  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const validationErrors = validateForm(formData);
      if (validationErrors) {
        setErrors(validationErrors);
        return;
      }
  
      const response = await axios.post('http://localhost:5077/api/Auth/registration', formData, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      
      console.log('Регистрация успешна', response.data);

      navigate(`/`);

    } catch (error) {
      console.error('Ошибка регистрации', error);
      if (error.response) {
        console.error('Ошибка от сервера:', error.response.data);
      } else if (error.request) {
        console.error('Не удалось получить ответ от сервера:', error.request);
      } else {
        console.error('Ошибка:', error.message);
      }
    }
  };
  

    return (
    <div className="registration-page">
      <div className="registration-container">
        <h2 className="registration-header">Регистрация</h2>

        {message && <div className="message">{message}</div>}

        <form onSubmit={handleSubmit}>
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
          <InputField
            label="Подтвердить пароль"
            type="password"
            name="confirmPassword"
            value={formData.confirmPassword}
            onChange={handleChange}
            error={errors.confirmPassword}
          />
          <InputField
            label="Имя"
            type="text"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            error={errors.firstName}
          />
          <InputField
            label="Фамилия"
            type="text"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            error={errors.lastName}
          />
          <InputField
            label="Группа"
            type="text"
            name="group"
            value={formData.group}
            onChange={handleChange}
            error={errors.group}
          />
          {/* <Button label="Зарегистрироваться" buttonType="submit"/> */}
          <Button label={isSubmitting ? "Отправка..." : "Зарегистрироваться"} buttonType="submit" disabled={isSubmitting} />
        </form>
        <div className="login-redirect">
          Уже есть аккаунт? <a href="/login">Войти</a>
        </div>
      </div>
    </div>
  );
}