import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../common/table/table";
import Pagination from "../../common/pagination/pagination";
import { jwtDecode } from "jwt-decode";

export default function TicketsPage() {
  const [tickets, setTickets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [ticketsPerPage] = useState(10);
  const token = localStorage.getItem("token");
  const decodedToken = jwtDecode(token);
  const userId = decodedToken.id;

  useEffect(() => {
    console.log(token);
    console.log(userId);
    const fetchTicketsAndUserRole = async () => {
      try {
        // Получаем данные пользователя
        const userResponse = await axios.get(
          `http://localhost:5077/api/studentprofile/${userId}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        const response = await axios.get(`http://localhost:5077/api/tickets`, {
          withCredentials: true,
        });

        const filteredTickets =
          userResponse.data.userType === 0
            ? response.data.filter(
                (ticket) => String(ticket.userId) === String(userId)
              )
            : response.data;
        console.log(filteredTickets);

        const formattedTickets = filteredTickets.map((ticket) => ({
          ...ticket,
          date: new Date(ticket.date).toLocaleDateString("ru-RU", {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
          }),
        //  
        
        //хочу заменить типы и статусы цыфры на слова, но пока не вышло
        }));

        setTickets(formattedTickets);
        setLoading(false);
      } catch (error) {
        console.error("Ошибка при получении тикетов:", error);
        setLoading(false);
      }
    };

    fetchTicketsAndUserRole();
  }, []);

  const indexOfLastTicket = currentPage * ticketsPerPage;
  const indexOfFirstTicket = indexOfLastTicket - ticketsPerPage;
  const currentTickets = tickets.slice(indexOfFirstTicket, indexOfLastTicket);

  const handleTicketClick = (ticketId) => {
    console.log(`Ticket ID: ${ticketId}`);
  };

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  if (loading) {
    return <p>Загрузка тикетов...</p>;
  }

  const columns = ["ID", "Name", "Subject", "Type", "Status", "Date"];

  return (
    <div>
      <h2>Список тикетов</h2>
      <Table
        columns={columns}
        data={currentTickets}
        onRowClick={handleTicketClick}
      />
      <Pagination
        itemsPerPage={ticketsPerPage}
        totalItems={tickets.length}
        paginate={paginate}
        currentPage={currentPage}
      />
    </div>
  );
}
