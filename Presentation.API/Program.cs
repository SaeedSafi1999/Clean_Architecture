using Core.Application.Extensions;
using Infrestructure.Persistance.Extensions;
using Presentation.API.Extension;
using WebFramework.Configuration;
//using WebFramework.Middlewares;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//add Swagger and auth dependency
builder.Services.AddSwaggerDependency();
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add Policy
builder.Services.AddCors(options => options.AddPolicy("myPol", builder =>
{
    builder.SetIsOriginAllowed(x => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

//Add Services Related To Application Layer 
builder.Services.AddApplicationServices(builder.Configuration);

//Add Services Related To Persistence Infrastructure layer
builder.Services.AddPersistanceInfrestructurelayarServcies();

var app = builder.Build();

app.IntializeDatabase();

//app.UseCustomExceptionHandler();

app.UseHsts(app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("myPol");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
