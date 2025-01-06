using EmpleadosAPI.Core.Entities;
using EmpleadosAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosAPI.Core.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<Empleado> GetEmpleadoByIdAsync(int id)
        {
            return await _empleadoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Empleado>> GetAllEmpleadosAsync()
        {
            return await _empleadoRepository.GetAllAsync();
        }

        public async Task<int> AddEmpleadoAsync(Empleado empleado)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(empleado.Nombres))
                throw new ArgumentException("El nombre del empleado es requerido.");

            if (string.IsNullOrWhiteSpace(empleado.DPI))
                throw new ArgumentException("El DPI del empleado es requerido.");

            if (empleado.FechaNacimiento >= DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento no puede ser en el futuro.");

            if (empleado.FechaIngreso > DateTime.Today)
                throw new ArgumentException("La fecha de ingreso no puede ser en el futuro.");

            return await _empleadoRepository.AddAsync(empleado);
        }

        public async Task UpdateEmpleadoAsync(Empleado empleado)
        {
            // Validaciones (similares a las de AddEmpleadoAsync)
            if (string.IsNullOrWhiteSpace(empleado.Nombres))
                throw new ArgumentException("El nombre del empleado es requerido.");

            if (string.IsNullOrWhiteSpace(empleado.DPI))
                throw new ArgumentException("El DPI del empleado es requerido.");

            if (empleado.FechaNacimiento >= DateTime.Today)
                throw new ArgumentException("La fecha de nacimiento no puede ser en el futuro.");

            if (empleado.FechaIngreso > DateTime.Today)
                throw new ArgumentException("La fecha de ingreso no puede ser en el futuro.");

            await _empleadoRepository.UpdateAsync(empleado);
        }

        public async Task<IEnumerable<Empleado>> GetEmpleadosByDepartamentoAsync(int departamentoId)
        {
            return await _empleadoRepository.GetByDepartamentoAsync(departamentoId);
        }

        public async Task<IEnumerable<Empleado>> GetEmpleadosByFechaIngresoRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha de inicio debe ser anterior o igual a la fecha fin.");

            return await _empleadoRepository.GetByFechaIngresoRangoAsync(fechaInicio, fechaFin);
        }
    }
}
