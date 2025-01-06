using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpleadosAPI.Core.Entities;
using EmpleadosAPI.Core.Interfaces;
using EmpleadosAPI.Infrastructure.Data;

namespace EmpleadosAPI.Infrastructure.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public EmpleadoRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Empleado> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_GetEmpleadoById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapEmpleadoFromReader(reader);
            }
            return null;
        }

        public async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            var empleados = new List<Empleado>();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SELECT * FROM Empleados", connection);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                empleados.Add(MapEmpleadoFromReader(reader));
            }
            return empleados;
        }

        public async Task<int> AddAsync(Empleado empleado)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_InsertEmpleado", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
            command.Parameters.AddWithValue("@DPI", empleado.DPI);
            command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
            command.Parameters.AddWithValue("@Sexo", empleado.Sexo);
            command.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
            command.Parameters.AddWithValue("@Edad", empleado.Edad);
            command.Parameters.AddWithValue("@Direccion", (object)empleado.Direccion ?? DBNull.Value);
            command.Parameters.AddWithValue("@NIT", (object)empleado.NIT ?? DBNull.Value);
            command.Parameters.AddWithValue("@DepartamentoId", empleado.DepartamentoId);
            command.Parameters.AddWithValue("@Estatus", empleado.Estatus);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_UpdateEmpleado", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
            command.Parameters.AddWithValue("@DPI", empleado.DPI);
            command.Parameters.AddWithValue("@FechaNacimiento", empleado.FechaNacimiento);
            command.Parameters.AddWithValue("@Sexo", empleado.Sexo);
            command.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
            command.Parameters.AddWithValue("@Direccion", (object)empleado.Direccion ?? DBNull.Value);
            command.Parameters.AddWithValue("@NIT", (object)empleado.NIT ?? DBNull.Value);
            command.Parameters.AddWithValue("@DepartamentoId", empleado.DepartamentoId);
            command.Parameters.AddWithValue("@Estatus", empleado.Estatus);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Empleado>> GetByDepartamentoAsync(int departamentoId)
        {
            var empleados = new List<Empleado>();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_GetEmpleadosByDepartamento", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@DepartamentoId", departamentoId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                empleados.Add(MapEmpleadoFromReader(reader));
            }
            return empleados;
        }

        public async Task<IEnumerable<Empleado>> GetByFechaIngresoRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var empleados = new List<Empleado>();
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("sp_GetEmpleadosByFechaIngreso", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            command.Parameters.AddWithValue("@FechaFin", fechaFin);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                empleados.Add(MapEmpleadoFromReader(reader));
            }
            return empleados;
        }

        private Empleado MapEmpleadoFromReader(SqlDataReader reader)
        {
            return new Empleado
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                DPI = reader.GetString(reader.GetOrdinal("DPI")),
                FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
                Sexo = reader.GetString(reader.GetOrdinal("Sexo")),
                FechaIngreso = reader.GetDateTime(reader.GetOrdinal("FechaIngreso")),
                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                NIT = reader.IsDBNull(reader.GetOrdinal("NIT")) ? null : reader.GetString(reader.GetOrdinal("NIT")),
                DepartamentoId = reader.GetInt32(reader.GetOrdinal("DepartamentoId"))
            };
        }
    }
}