import React, { useState, useEffect } from "react";
import ScheduleGrid from "../scheduleGrid/scheduleGrid";
import "./ReservationPage.css";

const ReservationPage = () => {
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
      const today = new Date();
      const weekDays = ["Вс", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб"];
      const todayIndex = today.getDay(); // Индекс текущего дня

      // Генерируем массив с текущей датой начиная с сегодняшнего дня
      return [
        ...weekDays.slice(todayIndex),  // Дни начиная с текущего
        ...weekDays.slice(0, todayIndex), // Дни до текущего
      ];
    };

    setDays(generateDaysOfWeek());
  }, []);

  return (
    <div className="reservation-page">
      <h1 className="title">Резервация стиральных машин</h1>
      {days.length > 0 && (
        <ScheduleGrid
          washers={washers}
          days={days}
          timeSlots={timeSlots}
          defaultDay={days[0]} // Передаем текущий день
        />
      )}
    </div>
  );
};

export default ReservationPage;
