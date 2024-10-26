import React, { useState } from "react";
import "./modal.css";

const Modal = ({ isOpen, onClose, onSubmit, ticket }) => {
  const [response, setResponse] = useState("");

  if (!isOpen) return null;

  const handleSubmit = () => {
    onSubmit({
      response,
    });
    setResponse(""); // Очищаем поле после отправки
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>Ответ на тикет #{ticket.id}</h2>
        <textarea
          value={response}
          onChange={(e) => setResponse(e.target.value)}
          placeholder="Введите ваш ответ..."
        />
        <button-modal onClick={handleSubmit}>Отправить ответ</button-modal>
        <button-modal onClick={onClose}>Закрыть</button-modal>
      </div>
    </div>
  );
};

export default Modal;
