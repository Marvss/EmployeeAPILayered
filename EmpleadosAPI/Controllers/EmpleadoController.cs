using EmpleadosAPI.Core.Entities;
using EmpleadosAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpleadosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            var empleados = await _empleadoService.GetAllEmpleadosAsync();
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _empleadoService.GetEmpleadoByIdAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return Ok(empleado);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEmpleado(Empleado empleado)
        {
            var id = await _empleadoService.AddEmpleadoAsync(empleado);
            return CreatedAtAction(nameof(GetEmpleado), new { id = id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, [FromBody] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return BadRequest("ID does not match the employee object ID.");
            }

            try
            {
                await _empleadoService.UpdateEmpleadoAsync(empleado);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating employee: {ex.Message}");
            }
        }

        [HttpGet("reporte")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetReporteEmpleados([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            var empleados = await _empleadoService.GetEmpleadosByFechaIngresoRangoAsync(fechaInicio, fechaFin);
            return Ok(empleados);
        }
    }
}
