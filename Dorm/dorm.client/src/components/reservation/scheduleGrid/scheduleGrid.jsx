import React, { useState } from 'react';
import axios from 'axios';
import './ScheduleGrid.css';

const ScheduleGrid = ({ washers, days, timeSlots, reservations }) => {
  const [selectedDay, setSelectedDay] = useState(days[0]); 
  const [currentReservations, setCurrentReservations] = useState(reservations); 

  const handleDayClick = (day) => {
    setSelectedDay(day);
    console.log(`Выбран день: ${day}`);
  };

  const formatDate = (date) => {
    const year = date.getUTCFullYear();
    const month = String(date.getUTCMonth() + 1).padStart(2, '0'); // Месяцы начинаются с 0
    const day = String(date.getUTCDate()).padStart(2, '0');
    const hours = String(date.getUTCHours()).padStart(2, '0');
    const minutes = String(date.getUTCMinutes()).padStart(2, '0');
    const seconds = String(date.getUTCSeconds()).padStart(2, '0');
    const milliseconds = String(date.getUTCMilliseconds()).padStart(3, '0'); // Получаем миллисекунды

    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}.${milliseconds}Z`; // Формат ISO 8601 с миллисекундами
  };

  const getTimesForReservation = (day, timeSlot) => {
    const [start, end] = timeSlot.split(' - ');
    const today = new Date();
    const selectedDate = new Date(today);
    const targetDay = days.indexOf(day);
    const delta = targetDay - today.getDay();
    selectedDate.setDate(today.getDate() + delta);
    const startTime = new Date(selectedDate);
    startTime.setHours(...start.split(':'));
    const endTime = new Date(selectedDate);
    endTime.setHours(...end.split(':'));

    return {
      startTime: formatDate(startTime), 
      endTime: formatDate(endTime),       
    };
  };

  const getReservationStatus = (washerId, timeSlot) => {
    const reservation = currentReservations.find(
      (r) =>
        r.washerId === washerId &&
        r.startTime.includes(timeSlot.split(' - ')[0])
    );

    if (!reservation) return 'available';
    return reservation.userId === 16 ? 'user-owned' : 'occupied';
  };

  const handleReservationClick = async (day, washerId, timeSlot) => {
    const status = getReservationStatus(washerId, timeSlot);
    if (status === 'available') {
      const { startTime, endTime } = getTimesForReservation(day, timeSlot);

      const reservationData = {
        washerId,
        startTime,   // В формате YYYY-MM-DDTHH:MM:SS.sssZ
        endTime,     // В формате YYYY-MM-DDTHH:MM:SS.sssZ
      };

      console.log('Отправляемые данные:', reservationData);

      try {
        const token = localStorage.getItem('token');
        const response = await axios.post(
          'http://localhost:5077/api/reservations',
          reservationData,
          {
            headers: {
              'Content-Type': 'application/json',
              Authorization: `Bearer ${token}`,
            },
            withCredentials: true,
          }
        );

        console.log('Резервация создана:', response.data);
        setCurrentReservations((prev) => [...prev, response.data]);
      } catch (error) {
        console.error(
          'Ошибка при создании резервации:',
          error.response ? error.response.data : error.message
        );
      }
    }
  };

  return (
    <div className="schedule-container">
      <div className="days-panel">
        {days.map((day, index) => (
          <button
            key={index}
            onClick={() => handleDayClick(day)}
            className={`day-button ${day === selectedDay ? 'active' : ''}`}
          >
            {day}
          </button>
        ))}
      </div>
      <table className="reservation-table">
        <thead>
          <tr>
            <th>Стиральные машины</th>
            {timeSlots.map((slot, index) => (
              <th key={index}>{slot}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {washers.map((washer) => (
            <tr key={washer.id}>
              <td>{washer.name}</td>
              {timeSlots.map((slot, index) => {
                const status = getReservationStatus(washer.id, slot);
                return (
                  <td
                    key={index}
                    className={`time-slot ${status}`}
                    onClick={() =>
                      handleReservationClick(selectedDay, washer.id, slot)
                    }
                  >
                    {status === 'user-owned' ? 'Ваша' : ''}
                  </td>
                );
              })}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ScheduleGrid;
