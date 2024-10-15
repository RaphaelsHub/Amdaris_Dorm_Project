import React, { useState, useEffect } from "react";
import ScheduleGrid from "../scheduleGrid/scheduleGrid";
import axios from "axios";
import "./ReservationPage.css";

const ReservationPage = () => {
    const [reservations, setReservations] = useState([]);
  const [washers] = useState([
    { id: 1, name: "Стиральная машина 1" },
    { id: 2, name: "Стиральная машина 2" },
    { id: 3, name: "Стиральная машина 3" },
  ]);

  const [days, setDays] = useState([]);
  const timeSlots = [
    "8:00 - 9:00",
    "9:00 - 10:00",
    "10:00 - 11:00",
    "11:00 - 12:00",
    "12:00 - 13:00",
    "13:00 - 14:00",
    "14:00 - 15:00",
    "15:00 - 16:00",
    "16:00 - 17:00",
    "17:00 - 18:00",
    "18:00 - 19:00",
    "19:00 - 20:00",
  ];

  useEffect(() => {
    const generateDaysOfWeek = () => {
      const today = new Date().getDay(); // 0 = Sunday, 1 = Monday, ...
      const weekDays = ["Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб"];
      return [...weekDays.slice(today), ...weekDays.slice(0, today)];
    };

    setDays(generateDaysOfWeek());

    // Получение всех резерваций с помощью axios
    axios
      .get("http://localhost:5077/api/reservations")
      .then((response) => setReservations(response.data))
      .catch((error) => console.error("Ошибка загрузки резерваций:", error));
  }, []);
  console.log(reservations);

  return (
    <div className="reservation-page">
      <h1 className="title">Резервация стиральных машин</h1>
      <ScheduleGrid 
      washers={washers} 
      days={days} 
      timeSlots={timeSlots} 
      reservations={reservations}
      />
    </div>
  );
};

export default ReservationPage;
