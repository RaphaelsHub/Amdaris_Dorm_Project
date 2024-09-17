import React, { useState } from "react";
import InputField from "../inputs/InputField";
import Button from "../common/button/Button";
import { validateForm } from "../../validation/FormValidation";
import './RegistrationForm.css'

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


  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };


  const handleSubmit = (e) => {
    e.preventDefault();

    const validationErrors = validateForm(formData);
    if (validationErrors) {
      setErrors(validationErrors);
    } else {
      setErrors({});
      console.log("Данные формы:", formData);
    }
  };

    return (
    <div className="registration-page">
      <div className="registration-container">
        <h2 className="registration-header">Регистрация</h2>

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
          <Button label="Зарегистрироваться" buttonType="submit"/>
        </form>
        <div className="login-redirect">
          Уже есть аккаунт? <a href="/login">Войти</a>
        </div>
      </div>
    </div>
  );
}