import React, { useState } from 'react';
import './modal.css';

const Modal = ({ isOpen, onClose, onSubmit, ticket, respondentData}) => {
  const [response, setResponse] = useState('');

  if (!isOpen) return null;
  const {id: respondentId, name: respondentName, email:respondentEmail} = respondentData;

  const handleSubmit = () => {
    onSubmit({
      respondentId,
      respondentName, // ID ответственного передается как пропс
      respondentEmail, // Email ответственного передается как пропс
      response, // Ответ из формы
      // Данные для отправки
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
        <button-modal onClick={handleSubmit}>Отправить ответ</button-modal>
        <button-modal onClick={onClose}>Закрыть</button-modal>
      </div>
    </div>
  );
};

export default Modal;
