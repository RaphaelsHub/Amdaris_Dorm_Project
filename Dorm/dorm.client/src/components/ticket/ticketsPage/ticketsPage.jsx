import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../common/table/table";
import Pagination from "../../common/pagination/pagination";
import { useNavigate } from "react-router-dom";

export default function TicketsPage() {
  const [tickets, setTickets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [ticketsPerPage] = useState(10);
  const [userRole, setUserRole] = useState(null);
  const navigate = useNavigate();

  
  const token = localStorage.getItem("token");

  useEffect(() => {
    if (!token) {
      console.error("No token found.");
      return;
    }

    const fetchTicketsAndUserRole = async () => {
      try {
        const userResponse = await axios.get(
          `http://localhost:5077/api/studentprofile`,
          {
            headers: {
              "Content-Type": "application/json",
              Authorization: `Bearer ${token}`,
            },
            withCredentials: true,
          }
        );

        setUserRole(userResponse.data.userType);

        const response = await axios.get(`http://localhost:5077/api/tickets`, {
          withCredentials: true,
        });

        setTickets(response.data);
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
    navigate(`/tickets/${ticketId}`, { state: { userRole } });
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
        userRole={userRole}
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
