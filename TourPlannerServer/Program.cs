using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Npgsql;
using Serilog;
using TourPlannerServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnectionString");
string dbPassword = builder.Configuration["DbPassword"];

var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString) {
    Password = dbPassword
};

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(npgsqlBuilder.ConnectionString));

// change the way swagger displays timespan to be in line with postgres
builder.Services.AddSwaggerGen(c => {
    c.MapType<TimeSpan>(() => new OpenApiSchema {
        Type = "string",
        Example = new OpenApiString("01:00:00")
    });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log_.log", rollingInterval: RollingInterval.Day,
                  outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


