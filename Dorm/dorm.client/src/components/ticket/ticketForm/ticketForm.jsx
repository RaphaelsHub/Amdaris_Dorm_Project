import axios from "axios";
import React, { useState, useEffect } from "react";
import InputField from "../../inputs/InputField";
import SelectField from "../../inputs/SelectField";
import Button from "../../common/button/Button";
import "./ticketForm.css"; // Импортируем стили
import { useNavigate, useParams } from "react-router-dom";

export default function TicketForm() {
  const [ticketFormData, setTicketFormData] = useState({
    name: "",
    group: "",
    room: "",
    type: 0,
    subject: "",
    description: "",
  });
  const { ticketId } = useParams();
  const [isEditMode, setIsEditMode] = useState(false);
  const [ticketErrors, setTicketErrors] = useState({});
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (ticketId) {
      setIsEditMode(true);
      const fetchTicketData = async() => {
        try {
          const response = await axios.get(`http://localhost:5077/api/tickets/${ticketId}`, {
            withCredentials: true
        });
        setTicketFormData({
          name: response.data.name,
          group: response.data.group,
          room: response.data.room,
          type: response.data.type,
          subject: response.data.subject,
          description: response.data.description
        });
        } catch(error) {
          console.error(" Ошибка при загрузке данных тикета: ", error.response ? error.response.data : error.message);
        }
      }
      fetchTicketData();
    }
    else {
    const token = localStorage.getItem("token");

    if (!token) {
      console.error("No token found.");
      return;
    }

    const fetchUserData = async () => {
      if (!token) {
        console.error("Token is missing");
        return;
      }

      try {
        const response = await axios.get(
          `http://localhost:5077/api/studentprofile`, //OOOOOKKKKKKKKKKKKK
          {
            headers: {
              "Content-Type": "application/json",
              Authorization: `Bearer ${token}`,
            },
            withCredentials: true,
          }
        );

        const userData = response.data;
        console.log(userData);

        setTicketFormData({
          ...ticketFormData,
          name: `${userData.firstName || ""} ${userData.lastName || ""}`,
          group: userData.group || "",
          room: userData.roomNumber || "",
        });
      } catch (error) {
        console.error(
          "Ошибка при получении данных пользователя:",
          error.response ? error.response.data : error.message
        );
      }
    };

    fetchUserData();}
  }, [ticketId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setTicketFormData({
      ...ticketFormData,
      [name]: name === "type" ? parseInt(value) : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newErrors = {};
    if (!ticketFormData.name)
      newErrors.name = "Поле обязательно для заполнения";
    if (!ticketFormData.subject)
      newErrors.subject = "Поле обязательно для заполнения";
    if (!ticketFormData.description)
      newErrors.description = "Поле обязательно для заполнения";
    if (!ticketFormData.room)
      newErrors.room = "Поле обязательно для заполнения";

    if (Object.keys(newErrors).length > 0) {
      setTicketErrors(newErrors);
      return;
    }

    try {
      setLoading(true);
      if (isEditMode) {
                
        const response = await axios.put(`http://localhost:5077/api/tickets/${ticketId}`, ticketFormData, {
            headers: {
                "Content-Type": "application/json",
            },
            withCredentials: true,
        });
        console.log("Товар обновлен:", response.data);
    } else
     { const response = await axios.post(
        "http://localhost:5077/api/tickets",
        ticketFormData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
          withCredentials: true,
        }
      );

      console.log("Тикет добавлен:", response.data);
      setTicketErrors({});}
      navigate(`/tickets`)
    } catch (error) {
      console.error(
        "Ошибка при добавлении тикета:",
        error.response ? error.response.data : error.message
      );

      if (error.response && error.response.data && error.response.data.errors) {
        setTicketErrors(error.response.data.errors);
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="ticket-form-page">
      <div className="ticket-form-container">
        <h2 className="ticket-form-header">Создать тикет</h2>
        <form onSubmit={handleSubmit} className="ticket-form">
          <InputField
            label="Имя"
            type="text"
            name="name"
            value={ticketFormData.name}
            onChange={handleChange}
            error={ticketErrors.name}
            className="input-field" // Добавляем класс
          />

          <InputField
            label="Группа"
            type="text"
            name="group"
            value={ticketFormData.group}
            onChange={handleChange}
            className="input-field" // Добавляем класс
          />

          <InputField
            label="Комната"
            type="text"
            name="room"
            value={ticketFormData.room}
            onChange={handleChange}
            className="input-field" // Добавляем класс
          />

          <SelectField
            label="Тип тикета"
            name="type"
            value={ticketFormData.type}
            options={[
              { value: "0", label: "Запрос" },
              { value: "1", label: "Жалоба" },
              { value: "2", label: "Предложение" },
            ]}
            onChange={handleChange}
            className="select-field" // Добавляем класс
          />

          <InputField
            label="Тема"
            type="text"
            name="subject"
            value={ticketFormData.subject}
            onChange={handleChange}
            error={ticketErrors.subject}
            className="input-field" // Добавляем класс
          />

          <InputField
            label="Описание"
            type="text"
            name="description"
            value={ticketFormData.description}
            onChange={handleChange}
            error={ticketErrors.description}
            className="input-field" // Добавляем класс
          />

          <Button
            label={isEditMode ? "Сохранить изменения" : "Создать тикет"}
            buttonType="submit"
            disabled={loading}
          />
        </form>
        {loading && <p className="loading-text">Загрузка...</p>} {}
      </div>
    </div>
  );
}
