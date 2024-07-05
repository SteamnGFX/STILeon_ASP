using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using STILeon.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cadenaConexionDB = builder.Configuration.GetConnectionString("conexion");

builder.Services.AddDbContext<StileonContext>(p => p.UseSqlServer(cadenaConexionDB));

builder.Services.AddCors(p => {
    p.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader(); // Permite cualquier encabezado
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();

