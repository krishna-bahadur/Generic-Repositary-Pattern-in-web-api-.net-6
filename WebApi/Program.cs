using DataAccessEF;
//using DataAccessEF.UnitOfWork;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//add dependency Injection of connection services
var connectionString = builder.Configuration.GetConnectionString("Conn");
builder.Services.AddDbContext<DBContext>(x => x.UseSqlServer(connectionString));

//add repositary interface and class
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
