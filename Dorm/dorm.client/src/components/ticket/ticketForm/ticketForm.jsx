import axios from "axios";
import React, { useState, useEffect } from 'react';
import InputField from '../../inputs/InputField';
import SelectField from '../../inputs/SelectField';
import Button from '../../common/button/Button';
import { jwtDecode } from 'jwt-decode';

export default function TicketForm() {
  const [ticketFormData, setTicketFormData] = useState({
    name: "",
    group: "",
    room: "",
    type: 0,
    subject: "",
    description: "",
  });

  const [ticketErrors, setTicketErrors] = useState({});
  const [loading, setLoading] = useState(false);

  // Получение данных пользователя
  useEffect(() => {
    const fetchUserData = async () => {
      const token = localStorage.getItem('token');
      console.log(token); // Выводим токен для проверки
      if (!token) {
        console.error("Token is missing");
        return;
      }

      const decodedToken = jwtDecode(token);
      const userId = decodedToken.id; 
      console.log(userId);

      try {
        const response = await axios.get(`http://localhost:5077/api/studentprofile/${userId}`, {
          headers: {
            Authorization: `Bearer ${token}`, // Добавьте токен в заголовок и нихера не помогает
          },
        });
        const userData = response.data;
        console.log(userData);

        // Заполнение формы данными пользователя
        setTicketFormData({
          ...ticketFormData,
          name: `${userData.firstName || ""} ${userData.lastName || ""}`, 
          group: userData.group || "",
          room: userData.roomNumber || "",
        });
      } catch (error) {
        console.error("Ошибка при получении данных пользователя:", error.response ? error.response.data : error.message);
      }
    };

    fetchUserData();
  }, []);

  // Обработчик изменений полей
  const handleChange = (e) => {
    const { name, value } = e.target;
    setTicketFormData({
      ...ticketFormData,
      [name]: name === "type" ? parseInt(value) : value,  
    });
  };

  // Обработчик отправки формы
  const handleSubmit = async (e) => {
    e.preventDefault();

    // Проверка на заполнение обязательных полей
    const newErrors = {};
    if (!ticketFormData.name) newErrors.name = "Поле обязательно для заполнения";
    if (!ticketFormData.subject) newErrors.subject = "Поле обязательно для заполнения";
    if (!ticketFormData.description) newErrors.description = "Поле обязательно для заполнения";

    // Если есть ошибки валидации на клиенте
    if (Object.keys(newErrors).length > 0) {
      setTicketErrors(newErrors);
      return;
    }

    try {
      setLoading(true);  // Начинаем загрузк
      const response = await axios.post("http://localhost:5077/api/tickets", ticketFormData, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem('token')}`, // Добавьте токен в заголовок
        },
        withCredentials: true,
      });

      console.log("Тикет добавлен:", response.data);
      setTicketErrors({});  // Очистим ошибки после успешной отправки

    } catch (error) {
      // Логирование ошибок сервера
      console.error("Ошибка при добавлении тикета:", error.response ? error.response.data : error.message);

      // Если есть ошибки валидации от сервера
      if (error.response && error.response.data && error.response.data.errors) {
        setTicketErrors(error.response.data.errors);  // Установим ошибки для отображения
      }
    } finally {
      setLoading(false);  // Окончание загрузки
    }
  };

  return (
    <div className='ticket-form-page'>
      <div className='ticket-form-container'>
        <h2 className='ticket-form-header'>Создать тикет</h2>

        <form onSubmit={handleSubmit}>
          <InputField
            label="Имя"
            type="text"
            name="name"
            value={ticketFormData.name}
            onChange={handleChange}
            error={ticketErrors.name}  // Отображение ошибки
          />

          <InputField
            label="Группа"
            type="text"
            name="group"
            value={ticketFormData.group}
            onChange={handleChange}
          />

          <InputField
            label="Комната"
            type="text"
            name="room"
            value={ticketFormData.room}
            onChange={handleChange}
          />

          <SelectField
            label="Тип тикета"
            name="type"
            value={ticketFormData.type}
            options={[
              { value: "0", label: "Запрос" },
              { value: "1", label: "Жалоба" },
              { value: "2", label: "Предложение" }
            ]}
            onChange={handleChange}
          />

          <InputField
            label="Тема"
            type="text"
            name="subject"
            value={ticketFormData.subject}
            onChange={handleChange}
            error={ticketErrors.subject}  // Отображение ошибки
          />

          <InputField
            label="Описание"
            type="text"
            name="description"
            value={ticketFormData.description}
            onChange={handleChange}
            error={ticketErrors.description}  // Отображение ошибки
          />

          <Button label={loading ? "Создание..." : "Создать тикет"} buttonType="submit" disabled={loading} />
        </form>

        {loading && <p>Загрузка...</p>}  {/* Уведомление о загрузке */}
      </div>
    </div>
  );
}
