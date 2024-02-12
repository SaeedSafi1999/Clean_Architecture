using Core.Application.Extensions;
using Infrestructure.Persistance.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Services Related To Application Layer 
builder.Services.AddApplicationServices();

//Add Services Related To Persistance Infrestructure layar
builder.Services.AddPersistanceInfrestructurelayarServcies();

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
