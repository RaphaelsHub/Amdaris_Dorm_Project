using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Dorm.BLL.Services;
using Dorm.DAL.Interfaces;
using Dorm.Domain.DTO.Laundry;
using Dorm.Domain.Entities.Laundry;
using Dorm.Domain.Responses;

namespace Dorm.BLL.Tests.Services
{
    public class WasherServiceTests
    {
        private readonly Mock<IWasherRepository> _mockWasherRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly WasherService _washerService;

        public WasherServiceTests()
        {
            _mockWasherRepository = new Mock<IWasherRepository>();
            _mockMapper = new Mock<IMapper>();
            _washerService = new WasherService(_mockWasherRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnSuccessResponse_WhenReservationIsCreated()
        {
            // Arrange
            var reservationDto = new ReservationDto { /* Инициализация данных для теста */ };
            var reservation = new Reservation { /* Инициализация Reservation для мока */ };
            _mockWasherRepository.Setup(repo => repo.Create(It.IsAny<Reservation>()))
                .Returns(Task.FromResult(true));
            _mockMapper.Setup(m => m.Map<Reservation>(reservationDto)).Returns(reservation);
            _mockMapper.Setup(m => m.Map<ReservationDto>(reservation)).Returns(reservationDto);

            // Act
            var response = await _washerService.Create(reservationDto);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal("Success.", response.Description);
        }

        [Fact]
        public async Task Create_ShouldReturnErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var reservationDto = new ReservationDto { /* Инициализация данных для теста */ };
            _mockWasherRepository.Setup(repo => repo.Create(It.IsAny<Reservation>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var response = await _washerService.Create(reservationDto);

            // Assert
            Assert.Null(response.Data);
            Assert.Equal("Test exception", response.Description);
            Assert.False(response.Data != null);
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccessResponse_WhenReservationIsDeleted()
        {
            // Arrange
            int reservationId = 1;
            var reservation = new Reservation();
            _mockWasherRepository.Setup(repo => repo.GetById(reservationId))
                .ReturnsAsync(reservation);
            _mockWasherRepository.Setup(repo => repo.Delete(reservation))
                .Returns(Task.FromResult(true));

            // Act
            var response = await _washerService.Delete(reservationId);

            // Assert
            Assert.True(response.Data);
            Assert.Equal("Success.", response.Description);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundResponse_WhenReservationDoesNotExist()
        {
            // Arrange
            int reservationId = 1;
            _mockWasherRepository.Setup(r => r.GetById(reservationId)).ReturnsAsync((Reservation)null);

            // Act
            var result = await _washerService.Delete(reservationId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Data);
            Assert.Equal($"Reservation with ID {reservationId} not found.", result.Description);
        }

        [Fact]
        public async Task GetAll_ShouldReturnSuccessResponse_WhenReservationsExist()
        {
            // Arrange
            var reservations = new List<Reservation> { new Reservation(), new Reservation() };
            var reservationDtos = new List<ReservationDto> { new ReservationDto(), new ReservationDto() };

            _mockWasherRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations);
            _mockMapper.Setup(m => m.Map<IEnumerable<ReservationDto>>(reservations)).Returns(reservationDtos);

            // Act
            var response = await _washerService.GetAll();

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(reservationDtos.Count, response.Data.Count());
            Assert.Equal("Success.", response.Description);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNotFoundResponse_WhenNoReservationsExist()
        {
            // Arrange
            var reservations = new List<Reservation>();
            _mockWasherRepository.Setup(repo => repo.GetAll()).ReturnsAsync(reservations);

            // Act
            var response = await _washerService.GetAll();

            // Assert
            Assert.Null(response.Data);
            Assert.Equal("Reservations not found.", response.Description);
        }

        [Fact]
        public async Task GetAllByUserId_ShouldReturnSuccessResponse_WhenReservationsExist()
        {
            // Arrange
            int userId = 1;
            var reservations = new List<Reservation> { new Reservation(), new Reservation() };
            var reservationDtos = new List<ReservationDto> { new ReservationDto(), new ReservationDto() };

            _mockWasherRepository.Setup(repo => repo.GetAllByUserId(userId)).ReturnsAsync(reservations);
            _mockMapper.Setup(m => m.Map<IEnumerable<ReservationDto>>(reservations)).Returns(reservationDtos);

            // Act
            var response = await _washerService.GetAllByUserId(userId);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(reservationDtos.Count, response.Data.Count());
            Assert.Equal("Success.", response.Description);
        }

        [Fact]
        public async Task GetAllByUserId_ShouldReturnNotFoundResponse_WhenNoReservationsExist()
        {
            // Arrange
            int userId = 1;
            var reservations = new List<Reservation>();
            _mockWasherRepository.Setup(repo => repo.GetAllByUserId(userId)).ReturnsAsync(reservations);

            // Act
            var response = await _washerService.GetAllByUserId(userId);

            // Assert
            Assert.Null(response.Data);
            Assert.Equal("Reservations not found.", response.Description);
        }

        [Fact]
        public async Task GetById_ShouldReturnSuccessResponse_WhenReservationExists()
        {
            // Arrange
            int reservationId = 1;
            var reservation = new Reservation();
            var reservationDto = new ReservationDto();

            _mockWasherRepository.Setup(repo => repo.GetById(reservationId)).ReturnsAsync(reservation);
            _mockMapper.Setup(m => m.Map<ReservationDto>(reservation)).Returns(reservationDto);

            // Act
            var response = await _washerService.GetById(reservationId);

            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal("Success.", response.Description);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFoundResponse_WhenReservationDoesNotExist()
        {
            // Arrange
            int reservationId = 1;
            _mockWasherRepository.Setup(repo => repo.GetById(reservationId)).ReturnsAsync((Reservation)null);

            // Act
            var response = await _washerService.GetById(reservationId);

            // Assert
            Assert.Null(response.Data);
            Assert.Equal($"Ticket with ID {reservationId} not found.", response.Description);
        }

        [Fact]
        public async Task HasReservation_ShouldReturnTrue_WhenReservationExistsInTimeRange()
        {
            // Arrange
            int washerId = 1;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddHours(1);

            // Создаем список резервирований
            var reservations = new List<Reservation>
    {
        new Reservation { /* Инициализация свойств, если необходимо */ }
    };

            // Настройка мока для возврата списка резервирований
            _mockWasherRepository.Setup(repo => repo.HasReservation(washerId, startTime, endTime))
                .ReturnsAsync(reservations); // Возвращаем коллекцию вместо булевого значения

            // Act
            var response = await _washerService.HasReservation(washerId, startTime, endTime);

            // Assert
            Assert.True(response.Data); // Проверка, что Data равно true, если есть хотя бы одно резервирование
            Assert.Equal("Success.", response.Description); // Проверка описания
        }



        [Fact]
        public async Task HasReservation_ShouldReturnFalse_WhenNoReservationExistsInTimeRange()
        {
            // Arrange
            int washerId = 1;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now.AddHours(1);

            // Настройка мока для возврата пустого списка
            _mockWasherRepository.Setup(repo => repo.HasReservation(washerId, startTime, endTime))
                .ReturnsAsync(new List<Reservation>()); // Возвращаем пустой список

            // Act
            var response = await _washerService.HasReservation(washerId, startTime, endTime);

            // Assert
            Assert.False(response.Data); // Проверка, что Data равно false
            Assert.Equal("Success.", response.Description); // Проверка описания
        }


    }
}
