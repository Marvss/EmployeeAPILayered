using EmpleadosAPI.Core.Entities;
using EmpleadosAPI.Core.Interfaces;
using EmpleadosAPI.Core.Services;
using Moq;

namespace Empleados.API.Tests
{
    [TestFixture]
    public class EmpleadoServiceTests
    {
        private Mock<IEmpleadoRepository> _mockRepository;
        private EmpleadoService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IEmpleadoRepository>();
            _service = new EmpleadoService(_mockRepository.Object);
        }

        [Test]
        public async Task GetEmpleadoByIdAsync_ExistingId_ReturnsEmpleado()
        {
            // Arrange
            var empleadoId = 1;
            var expectedEmpleado = new Empleado { Id = empleadoId, Nombres = "John Doe" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(empleadoId)).ReturnsAsync(expectedEmpleado);

            // Act
            var result = await _service.GetEmpleadoByIdAsync(empleadoId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmpleado.Id, result.Id);
            Assert.AreEqual(expectedEmpleado.Nombres, result.Nombres);
        }

        [Test]
        public async Task AddEmpleadoAsync_ValidEmpleado_ReturnsId()
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
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Empleado>())).ReturnsAsync(expectedId);

            // Act
            var result = await _service.AddEmpleadoAsync(newEmpleado);

            // Assert
            Assert.AreEqual(expectedId, result);
        }

        [Test]
        public void AddEmpleadoAsync_InvalidEmpleado_ThrowsArgumentException()
        {
            // Arrange
            var invalidEmpleado = new Empleado(); // Sin datos requeridos

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.AddEmpleadoAsync(invalidEmpleado));
        }

        [Test]
        public async Task GetEmpleadosByDepartamentoAsync_ExistingDepartamento_ReturnsEmpleados()
        {
            // Arrange
            var departamentoId = 1;
            var expectedEmpleados = new List<Empleado>
            {
                new Empleado { Id = 1, Nombres = "John Doe", DepartamentoId = departamentoId },
                new Empleado { Id = 2, Nombres = "Jane Doe", DepartamentoId = departamentoId }
            };
            _mockRepository.Setup(repo => repo.GetByDepartamentoAsync(departamentoId)).ReturnsAsync(expectedEmpleados);

            // Act
            var result = await _service.GetEmpleadosByDepartamentoAsync(departamentoId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmpleados.Count, result.Count());
        }
    }
}