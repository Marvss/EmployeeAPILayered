using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleadosAPI.Core.Entities;

namespace EmpleadosAPI.Core.Interfaces
{
    public interface IEmpleadoService
    {
        Task<Empleado> GetEmpleadoByIdAsync(int id);
        Task<IEnumerable<Empleado>> GetAllEmpleadosAsync();
        Task<int> AddEmpleadoAsync(Empleado empleado);
        Task UpdateEmpleadoAsync(Empleado empleado);
        Task<IEnumerable<Empleado>> GetEmpleadosByDepartamentoAsync(int departamentoId);
        Task<IEnumerable<Empleado>> GetEmpleadosByFechaIngresoRangoAsync(DateTime fechaInicio, DateTime fechaFin);
    }

}
