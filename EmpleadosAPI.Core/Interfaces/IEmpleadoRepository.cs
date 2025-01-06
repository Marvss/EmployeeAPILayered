using EmpleadosAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosAPI.Core.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<Empleado> GetByIdAsync(int id);
        Task<IEnumerable<Empleado>> GetAllAsync();
        Task<int> AddAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task<IEnumerable<Empleado>> GetByDepartamentoAsync(int departamentoId);
        Task<IEnumerable<Empleado>> GetByFechaIngresoRangoAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
