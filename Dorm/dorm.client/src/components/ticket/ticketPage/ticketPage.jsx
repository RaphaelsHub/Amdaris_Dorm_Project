import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate, useParams, useLocation } from "react-router-dom";
import Modal from "../ticketResponseForm/modal";
import "./ticketPage.css";

export default function TicketPage() {
  const { ticketId } = useParams();
  const navigate = useNavigate();
  const location = useLocation();
  const { userRole, respondentData } = location.state || {};
  const [ticketData, setTicketData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    const fetchTicketData = async () => {
      try {
        const response = await axios.get(
          `http://localhost:5077/api/tickets/${ticketId}`,
          {
            withCredentials: true,
          }
        );
        setTicketData(response.data);
      } catch (error) {
        setError("Ошибка при загрузке тикета");
      } finally {
        setLoading(false);
      }
    };

    fetchTicketData();
  }, [ticketId]);

  const handleEdit = () => {
    navigate(`/tickets/edit/${ticketId}`);
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:5077/api/tickets/${ticketId}`, {
        withCredentials: true,
      });
      navigate("/tickets");
    } catch (error) {
      setError("Ошибка при удалении тикета");
    }
  };

  const handleReply = () => {
    setIsModalOpen(true);
  };

  const handleModalClose = () => {
    setIsModalOpen(false);
  };

  const handleResponseSubmit = async (responseData) => {
    try {
      
      await axios.put(
        `http://localhost:5077/api/tickets/${ticketId}`,
        responseData,
        {
          withCredentials: true,
        }
      );
      setTicketData((prevData) => ({
        ...prevData,
        ...responseData,
      }));
      handleModalClose();
    } catch (error) {
      setError("Ошибка при отправке ответа");
    }
  };

  if (loading) return <p>Загрузка...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="ticket-container">
      <h1>Тикет №{ticketData.id}</h1>
      <div className="ticket-info">
        <p>
          <strong>ФИО:</strong> {ticketData.name}
        </p>
        <p>
          <strong>Группа:</strong> {ticketData.group}
        </p>
        <p>
          <strong>Комната:</strong> {ticketData.room}
        </p>
        <p>
          <strong>Тип:</strong>
          {ticketData.type === 0
            ? " Запрос"
            : ticketData.type === 1
              ? " Жалоба"
              : " Предложение"}
        </p>
        <p>
          <strong>Тема:</strong> {ticketData.subject}
        </p>
        <p>
          <strong>Описание:</strong> {ticketData.description}
        </p>
        <p>
          <strong>Статус:</strong>{" "}
          {ticketData.status === 0
            ? " Отправен"
            : ticketData.status === 1
              ? " В процессе"
              : " Закрыт"}
        </p>
        <p>
          <strong>Дата создания:</strong>{" "}
          {new Date(ticketData.date).toLocaleString()}
        </p>
        <p>
          <strong>Ответственный:</strong>{" "}
          {ticketData.respondentName || "Не назначен"}
        </p>
        <p>
          <strong>Email Ответственного:</strong>{" "}
          {ticketData.respondentEmail || "Не указан"}
        </p>
      </div>
      <div className="ticket-buttons">
        
          <button className="edit-button" onClick={handleEdit}>
            Редактировать
          </button>
        
        <button className="delete-button" onClick={handleDelete}>
          Удалить
        </button>

        
        {userRole >= 2 && (
          <button className="reply-button" onClick={handleReply}>
            Ответить
          </button>
        )}
      </div>

      <Modal
        isOpen={isModalOpen}
        onClose={handleModalClose}
        onSubmit={handleResponseSubmit}
        ticket={ticketData}
        respondentData={respondentData}
      />
    </div>
  );
}
