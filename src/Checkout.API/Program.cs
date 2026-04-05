using Checkout.API.Application.Commands;
using Checkout.API.Application.Interfaces;
using Checkout.API.Domain.Interfaces;
using Checkout.API.Infrastructure.Data.Contexts;
using Checkout.API.Infrastructure.Identity;
using Checkout.API.Infrastructure.Messaging;
using Checkout.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Injection dependencies
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEventBus, OutboxEventBus>();
builder.Services.AddScoped<CreateOrderCommandHandler>();
builder.Services.AddTransient<IUserContext, MockUserContext>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
