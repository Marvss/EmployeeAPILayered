using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosAPI.Core.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string? Nombres { get; set; }
        public string? DPI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string? Direccion { get; set; }
        public string? NIT { get; set; }
        public int DepartamentoId { get; set; }
        public bool Estatus { get; set; }
        public int Edad => CalcularEdad();

        private int CalcularEdad()
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - FechaNacimiento.Year;
            if (FechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
            return edad;
        }
    }
}
