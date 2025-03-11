using FluentValidation;
using mac_api.mac_address;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// db
var connectionString = "server=localhost;database=mac_addresses;user=root;";
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<MacAddressContext>(options => options.UseMySql(connectionString, serverVersion));

// validator
builder.Services.AddScoped<IValidator<MacAddress>, MacAddressValidator>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
