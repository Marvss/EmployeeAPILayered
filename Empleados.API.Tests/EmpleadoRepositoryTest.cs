using EmpleadosAPI.Core.Entities;
using EmpleadosAPI.Core.Interfaces;
using EmpleadosAPI.Core.Services;
using EmpleadosAPI.Infrastructure.Data;
using EmpleadosAPI.Infrastructure.Repositories;
using Moq;
using System.Data.SqlClient;

namespace Empleados.API.Tests
{
    [TestFixture]
    public class EmpleadoRepositoryTests
    {
        private Mock<SqlConnectionFactory> _mockConnectionFactory;
        private EmpleadoRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _mockConnectionFactory = new Mock<SqlConnectionFactory>();
            _repository = new EmpleadoRepository(_mockConnectionFactory.Object);
        }

        [Test]
        public async Task GetByIdAsync_ExistingId_ReturnsEmpleado()
        {
            // Arrange
            var empleadoId = 1;
            var mockConnection = new Mock<SqlConnection>();
            var mockCommand = new Mock<SqlCommand>();
            var mockDataReader = MockDataReader();

            _mockConnectionFactory.Setup(factory => factory.CreateConnection()).Returns(mockConnection.Object);
            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteReaderAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(mockDataReader.Object);

            // Act
            var result = await _repository.GetByIdAsync(empleadoId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(empleadoId, result.Id);
            Assert.AreEqual("John Doe", result.Nombres);
        }

        [Test]
        public async Task AddAsync_ValidEmpleado_ReturnsId()
        {
            // Arrange
            var newEmpleado = new Empleado
            {
                Nombres = "Jane Doe",
                DPI = "1234567890",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Sexo = "F",
                FechaIngreso = DateTime.Today
            };
            var expectedId = 1;

            var mockConnection = new Mock<SqlConnection>();
            var mockCommand = new Mock<SqlCommand>();

            _mockConnectionFactory.Setup(factory => factory.CreateConnection()).Returns(mockConnection.Object);
            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(cmd => cmd.ExecuteScalarAsync(It.IsAny<System.Threading.CancellationToken>())).ReturnsAsync(expectedId);

            // Act
            var result = await _repository.AddAsync(newEmpleado);

            // Assert
            Assert.AreEqual(expectedId, result);
        }

        private Mock<SqlDataReader> MockDataReader()
        {
            var mockDataReader = new Mock<SqlDataReader>();
            mockDataReader.Setup(r => r.Read()).Returns(true);
            mockDataReader.Setup(r => r.GetInt32(It.IsAny<int>())).Returns(1);
            mockDataReader.Setup(r => r.GetString(It.IsAny<int>())).Returns("John Doe");
            mockDataReader.Setup(r => r.GetDateTime(It.IsAny<int>())).Returns(new DateTime(1990, 1, 1));
            mockDataReader.Setup(r => r.IsDBNull(It.IsAny<int>())).Returns(false);
            return mockDataReader;
        }
    }
}
