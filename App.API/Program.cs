using App.API.Extension;
using App.Infrastructure.Extension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//using environment variable
//builder.Host.UseSerilog((hostContext, services, config) =>
//{
//    config.WriteTo.MongoDB(Environment.GetEnvironmentVariable("Logs"), "logs", LogEventLevel.Error);
//});

builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

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
