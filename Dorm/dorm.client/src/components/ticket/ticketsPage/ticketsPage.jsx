import React, { useState, useEffect } from "react";
import axios from "axios";
import Table from "../../common/table/table";
import Pagination from "../../common/pagination/pagination";
import { useNavigate } from 'react-router-dom';

export default function TicketsPage() {
  const [tickets, setTickets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [ticketsPerPage] = useState(10);
  const [userRole, setUserRole] = useState(null); 
  const [respondentData, setRespondentData] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (!token) {
      console.error("No token found.");
      return;
    }

    const fetchTicketsAndUserRole = async () => {
      try {
        const userResponse = await axios.get(
            `http://localhost:5077/api/studentprofile`, //3 - временно, потом убрать, для тестирования ответа
            {
              headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
              },
              withCredentials: true,
            }
        );

        const userId = userResponse.data.id; 
        setUserRole(userResponse.data.userType);

        const response = await axios.get(`http://localhost:5077/api/tickets`, {
          withCredentials: true,
        });

        const filteredTickets =
          userResponse.data.userType === 0
            ? response.data.filter(
                (ticket) => String(ticket.userId) === String(userId) 
              )
            : response.data;

        const formattedTickets = filteredTickets.map((ticket) => ({
          ...ticket,
          date: new Date(ticket.date).toLocaleDateString("ru-RU", {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
          }),
        }));
        
          const respondent = {
            id: userResponse.data.id,
            email: userResponse.data.email,
            name: userResponse.data.firstName + " " + userResponse.data.lastName,

          }
          if (userRole >=1)
          console.log(respondent)
          setRespondentData(respondent);
          
        
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
    navigate(`/tickets/${ticketId}`, { state: { userRole, respondentData } }); 
    console.log(`Ticket ID: ${ticketId}, User Role: ${userRole}, Respondent Data:${respondentData}`);
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
        respondentData={respondentData}
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
