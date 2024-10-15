import React from 'react';
import './ScheduleGrid.css';

const ScheduleGrid = ({ washers, days, timeSlots }) => {
  const handleDayClick = (day) => {
    // Логика для обработки клика по дню недели
    console.log(`Кликнули на ${day}`);
  };

  return (
    <div className="schedule-container">
      <div className="days-panel">
        {days.map((day, index) => (
          <button 
            key={index} 
            onClick={() => handleDayClick(day)}
            className="day-button"
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
          {washers.map((washer, washerIndex) => (
            <tr key={washerIndex}>
              <td>{washer.name}</td>
              {timeSlots.map((_, timeIndex) => (
                <td key={timeIndex} className="time-slot available">
                  {/* Здесь можно добавить логику отображения статуса резервирования */}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ScheduleGrid;
