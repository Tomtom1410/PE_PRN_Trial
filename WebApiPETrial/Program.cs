using Microsoft.EntityFrameworkCore;
using WebApiPETrial.Models;
using WebApiPETrial.Repositories;
using WebApiPETrial.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PRN_Sum22_B1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("prn_db")));
builder.Services.AddScoped<IOrderRepositories, OrderRepositories>();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
 builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
