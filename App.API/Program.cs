using App.API.Extension;
using App.Infrastructure.Extension;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, services, config) =>
{
    config.WriteTo.MongoDB(Environment.GetEnvironmentVariable("Logs"), "logs", LogEventLevel.Error);
});

// Add services to the container.
builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddDependencyInjection();

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
