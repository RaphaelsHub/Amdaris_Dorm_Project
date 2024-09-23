import axios from "axios";
import React, { useState, useEffect } from 'react';
import InputField from '../../inputs/InputField';
import SelectField from '../../inputs/SelectField';
import Button from '../../common/button/Button';

export default function TicketForm({ currentUserId }) {
  const [ticketFormData, setTicketFormData] = useState({
    userId: currentUserId,
    name: "",
    group: "",
    room: "",
    type: 0,
    subject: "",
    description: "",
  });

  const [ticketErrors, setTicketErrors] = useState({});
  const [loading, setLoading] = useState(true);

  
  useEffect(() => {

    //if (!currentUserId) return; //временно, пока айди не передается динамически

    const fetchUserData = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5077/api/studentprofile/2`// временно, пока айдишник не передается динамически
        );
        const userData = response.data;

        // Автозаполнение полей формы
        setTicketFormData((prevData) => ({
          ...prevData,
          name: `${userData.lastname} ${userData.firstName}` || "",
          group: userData.group || "",
          room: userData.roomNumber || "",
        }));
      } catch (error) {
        console.error(
          "Loading user data error:",
          error.response ? error.response.data : error.message
        );
      } finally {
        setLoading(false);
      }
    };

    fetchUserData();
  }, [currentUserId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setTicketFormData({
      ...ticketFormData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newErrors = {};
    if (!ticketFormData.name) newErrors.name = "Поле обязательно для заполнения";
    if (!ticketFormData.subject) newErrors.subject = "Поле обязательно для заполнения";
    if (!ticketFormData.description) newErrors.description = "Поле обязательно для заполнения";

    if (Object.keys(newErrors).length > 0) {
      setTicketErrors(newErrors);
      return;
    }

    try {
      const response = await axios.post("http://localhost:5077/api/tickets", ticketFormData, {
        headers: {
          "Content-Type": "application/json",
        },
        withCredentials: true,
      });
      console.log("Тикет добавлен:", response.data);
    } catch (error) {
      console.error("Ошибка при добавлении тикета:", error.response ? error.response.data : error.message);
    }
  };

//   if (loading) {
//     return <p>Загрузка данных пользователя...</p>;
//   }

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
            error={ticketErrors.name}
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
              { value: 0, label: "Запрос" },
              { value: 1, label: "Жалоба" },
              { value: 2, label: "Предложение" }
            ]}
            onChange={handleChange}
          />

          <InputField
            label="Тема"
            type="text"
            name="subject"
            value={ticketFormData.subject}
            onChange={handleChange}
            error={ticketErrors.subject}
          />

          <InputField
            label="Описание"
            type="text"
            name="description"
            value={ticketFormData.description}
            onChange={handleChange}
            error={ticketErrors.description}
          />

          <Button label="Создать тикет" buttonType="submit" />
        </form>
      </div>
    </div>
  );
}
