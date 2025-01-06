
using EmpleadosAPI.Core.Interfaces;
using EmpleadosAPI.Core.Services;
using EmpleadosAPI.Infrastructure.Data;
using EmpleadosAPI.Infrastructure.Repositories;

namespace EmpleadosAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddTransient<SqlConnectionFactory>(provider => new SqlConnectionFactory(connectionString));
            builder.Services.AddTransient<IEmpleadoRepository, EmpleadoRepository>();
            builder.Services.AddTransient<IEmpleadoService, EmpleadoService>();
            // Configuración de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            var app = builder.Build();
            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
