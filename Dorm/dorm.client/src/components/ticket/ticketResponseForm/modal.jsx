
import React, { useState } from 'react';
import './modal.css';
//тут бред пока, доделать
const Modal = ({ isOpen, onClose, onSubmit, ticket }) => {
  const [response, setResponse] = useState('');

  if (!isOpen) return null;

  const handleSubmit = () => {
    onSubmit({
    //   respondentId: ticket.userId, // Используйте актуальное значение
    //   respondentName: ticket.userName, // Используйте актуальное значение
    //   respondentEmail: ticket.userEmail, // Используйте актуальное значение
    //   response: response,
    });
    setResponse(''); // Очищаем поле после отправки
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
        <button onClick={handleSubmit}>Отправить ответ</button>
        <button onClick={onClose}>Закрыть</button>
      </div>
    </div>
  );
};

export default Modal;
