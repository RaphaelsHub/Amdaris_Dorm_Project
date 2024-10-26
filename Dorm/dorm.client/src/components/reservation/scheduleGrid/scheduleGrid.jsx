import React, { useState, useEffect } from "react";
import axios from "axios";
import "./ScheduleGrid.css";

const ScheduleGrid = ({ washers, days, timeSlots, defaultDay }) => {
  const [currentReservations, setCurrentReservations] = useState([]);
  const [currentUserId, setCurrentUserId] = useState(null);
  const [selectedDay, setSelectedDay] = useState(defaultDay); // Устанавливаем выбранный день

  useEffect(() => {
    const fetchData = async () => {
      try {
        const token = localStorage.getItem("token");
        if (!token) {
          console.error("No token found.");
          return;
        }

        const userResponse = await axios.get(
          "http://localhost:5077/api/studentprofile",
          {
            headers: { Authorization: `Bearer ${token}` },
            withCredentials: true,
          }
        );

        setCurrentUserId(userResponse.data.id);

        const reservationsResponse = await axios.get(
          "http://localhost:5077/api/reservations",
          {
            headers: { Authorization: `Bearer ${token}` },
            withCredentials: true,
          }
        );

        setCurrentReservations(reservationsResponse.data);
      } catch (error) {
        console.error("Ошибка при загрузке данных:", error);
      }
    };

    fetchData();
  }, []);

  const handleDayClick = (day) => setSelectedDay(day);

  const getReservationStatus = (washerId, timeSlot) => {
    const reservation = currentReservations.find(
      (r) =>
        r.washerId === washerId &&
        r.startTime.includes(timeSlot.split(" - ")[0]) &&
        isSameDay(r.startTime, selectedDay) // Сравниваем с выбранным днем
    );

    if (!reservation) return "available";
    return reservation.userId === currentUserId ? "user-owned" : "occupied";
  };

  const handleReservationClick = async (day, washerId, timeSlot) => {
    const status = getReservationStatus(washerId, timeSlot);
    if (status === "available") {
      const { startTime, endTime } = getTimesForReservation(day, timeSlot);
      const reservationData = { washerId, startTime, endTime };

      console.log("Попытка создания резервации:", reservationData); // Лог для отслеживания данных

      try {
        const token = localStorage.getItem("token");
        if (!token) {
          console.error("No token found.");
          return;
        }

        const response = await axios.post(
          "http://localhost:5077/api/reservations",
          reservationData,
          {
            headers: {
              "Content-Type": "application/json",
              Authorization: `Bearer ${token}`,
            },
            withCredentials: true,
          }
        );

        console.log("Резервация успешно создана:", response.data); // Лог успешного создания
        setCurrentReservations((prev) => [...prev, response.data]);
      } catch (error) {
        console.error("Ошибка при создании резервации:", error.message);

        if (error.response && error.response.status === 400) {
          console.error("Возможная причина: такая резервация уже существует."); // Лог о возможной причине
        }
      }
    } else {
      console.log(`Резервация недоступна: статус - ${status}`); // Лог для недоступных резервирований
    }
  };

  const getTimesForReservation = (day, timeSlot) => {
    const [start, end] = timeSlot.split(" - ");
    const today = new Date();
    const selectedDate = new Date(today); // Копируем текущую дату

    // Корректируем дату для выбранного дня
    selectedDate.setDate(today.getDate() + (days.indexOf(day) - today.getDay() + 1));
    
    // Установка времени начала и окончания
    const startTime = new Date(selectedDate);
    const endTime = new Date(selectedDate);
    
    // Устанавливаем часы и минуты для начала и окончания
    const [startHour, startMinute] = start.split(":");
    const [endHour, endMinute] = end.split(":");

    console.log (startHour, endHour);
    
    startTime.setUTCHours(parseInt(startHour), parseInt(startMinute), 0, 0);
    endTime.setUTCHours(parseInt(endHour), parseInt(endMinute), 0, 0);

    console.log(startTime, endTime);

    return {
      startTime: startTime.toISOString(),
      endTime: endTime.toISOString(),
      
    };
  };

  // Функция для проверки, совпадают ли дни
  const isSameDay = (dateStr, day) => {
    const date = new Date(dateStr);
    const today = new Date();
    const selectedDate = new Date(today);
    selectedDate.setDate(today.getDate() + (days.indexOf(day) - today.getDay() + 1));
    return (
      date.getFullYear() === selectedDate.getFullYear() &&
      date.getMonth() === selectedDate.getMonth() &&
      date.getDate() === selectedDate.getDate()
    );
  };

  return (
    <div className="schedule-container">
      <div className="days-panel">
        {days.map((day, index) => (
          <button
            key={index}
            onClick={() => handleDayClick(day)}
            className={`day-button ${day === selectedDay ? "active" : ""}`}
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
                    className={`time-slot ${status}`} // Добавление класса статуса
                    onClick={() =>
                      status === "available" &&
                      handleReservationClick(selectedDay, washer.id, slot)
                    }
                  >
                    {status === "user-owned" ? "Ваша" : ""}
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
