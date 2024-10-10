using System;
using Dorm.Domain.DTO;
using Dorm.Domain.Enum.Ticket;
using Xunit;

namespace Dorm.Domain.Tests
{
    public class TicketDtoTests
    {
        [Fact]
        public void TicketDto_ShouldInitialize_WithDefaultValues()
        {
            // Arrange & Act
            var ticketDto = new TicketDto();

            // Assert
            Assert.Equal(0, ticketDto.Id);
            Assert.Equal(0, ticketDto.UserId);
            Assert.Null(ticketDto.Name);
            Assert.Null(ticketDto.Group);
            Assert.Null(ticketDto.Room);
            Assert.Equal(default(TicketType), ticketDto.Type); 
            Assert.Null(ticketDto.Subject);
            Assert.Null(ticketDto.Description);
            Assert.Equal(default(TicketStatus), ticketDto.Status); 
            Assert.Equal(0, ticketDto.RespondentId);
            Assert.Null(ticketDto.RespondentName);
            Assert.Null(ticketDto.RespondentEmail);
            Assert.True(ticketDto.Date <= DateTime.UtcNow && ticketDto.Date >= DateTime.UtcNow.AddSeconds(-1)); // Проверяем, что дата близка к текущему времени UTC
            Assert.Null(ticketDto.Response);
            Assert.False(ticketDto.canEdit);
        }

        [Fact]
        public void TicketDto_ShouldAllowSettingProperties()
        {
            // Arrange
            var ticketDto = new TicketDto();
            var expectedDate = DateTime.UtcNow;

            // Act
            ticketDto.Id = 1;
            ticketDto.UserId = 2;
            ticketDto.Name = "TestName";
            ticketDto.Group = "TestGroup";
            ticketDto.Room = "101";
            ticketDto.Type = TicketType.REQUEST;
            ticketDto.Subject = "TestSubject";
            ticketDto.Description = "TestDescription";
            ticketDto.Status = TicketStatus.IN_PROCESS; 
            ticketDto.RespondentId = 3;
            ticketDto.RespondentName = "TestRespondentName";
            ticketDto.RespondentEmail = "test@example.com";
            ticketDto.Date = expectedDate;
            ticketDto.Response = "TestResponse";
            ticketDto.canEdit = true;

            // Assert
            Assert.Equal(1, ticketDto.Id);
            Assert.Equal(2, ticketDto.UserId);
            Assert.Equal("TestName", ticketDto.Name);
            Assert.Equal("TestGroup", ticketDto.Group);
            Assert.Equal("101", ticketDto.Room);
            Assert.Equal(TicketType.REQUEST, ticketDto.Type); 
            Assert.Equal("TestSubject", ticketDto.Subject);
            Assert.Equal("TestDescription", ticketDto.Description);
            Assert.Equal(TicketStatus.IN_PROCESS, ticketDto.Status); 
            Assert.Equal(3, ticketDto.RespondentId);
            Assert.Equal("TestRespondentName", ticketDto.RespondentName);
            Assert.Equal("test@example.com", ticketDto.RespondentEmail);
            Assert.Equal(expectedDate, ticketDto.Date);
            Assert.Equal("TestResponse", ticketDto.Response);
            Assert.True(ticketDto.canEdit);
        }
    }
}
