using Dorm.Domain.Entities.Ticket;
using Dorm.Domain.Enum.Ticket;
using System;
using Xunit;

namespace Dorm.Tests
{
    public class TicketTests
    {
        [Fact]
        public void CanCreateTicket_WithValidData()
        {
            // Arrange
            var ticket = new Ticket
            {
                Id = 1,
                UserId = 123,
                RespondentId = 456,
                RespondentName = "John Doe",
                RespondentEmail = "johndoe@example.com",
                Name = "TicketName",
                Group = "GroupName",
                Room = "Room 101",
                Type = TicketType.REQUEST, 
                Subject = "Subject of the ticket",
                Description = "Description of the ticket",
                Status = TicketStatus.SENT, 
                Date = new DateTime(2024, 9, 24),
                Response = "Response text"
            };

            // Act & Assert
            Assert.Equal(1, ticket.Id); 
            Assert.Equal(123, ticket.UserId);
            Assert.Equal(456, ticket.RespondentId);
            Assert.Equal("John Doe", ticket.RespondentName);
            Assert.Equal("johndoe@example.com", ticket.RespondentEmail);
            Assert.Equal("TicketName", ticket.Name);
            Assert.Equal("GroupName", ticket.Group);
            Assert.Equal("Room 101", ticket.Room);
            Assert.Equal(TicketType.REQUEST, ticket.Type); 
            Assert.Equal("Subject of the ticket", ticket.Subject);
            Assert.Equal("Description of the ticket", ticket.Description);
            Assert.Equal(TicketStatus.SENT, ticket.Status); 
            Assert.Equal(new DateTime(2024, 9, 24), ticket.Date);
            Assert.Equal("Response text", ticket.Response);
        }
    }
}
