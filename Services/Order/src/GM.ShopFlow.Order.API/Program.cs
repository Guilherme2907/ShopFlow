using GM.ShopFlow.Order.API.Endpoints;
using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;
using GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;
using GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;
using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using GM.ShopFlow.Order.Infra.Data;
using GM.ShopFlow.Order.Infra.Data.Context;
using GM.ShopFlow.Order.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Order.Application.IntegrationsEvents.EventHandlers;
using GM.ShopFlow.Order.Infra.ExternalServices.Services;
using GM.ShopFlow.Order.Infra.ExternalServices.Interfaces;
using GM.ShopFlow.Order.Infra.ExternalServices.Apis;
using RestEase;
using StackExchange.Redis;
using GM.ShopFlow.Order.Application.SettingModels;
using GM.ShopFlow.Shared.EventBus.Extensions;
using GM.ShopFlow.Shared.EventBusRabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"]!,
        ValidAudience = builder.Configuration["JwtConfig:Audience"]!,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SymmetricSecurityKey"]!)),
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
);

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateOrder).Assembly));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICreateOrder, CreateOrder>();
builder.Services.AddScoped<IGetOrders, GetOrders>();
builder.Services.AddScoped<ICreateCustomer, CreateCustomer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductStockRepository, RedisProductStockRepository>();
builder.Services.AddScoped<IProductStockService, ProductStockService>();

builder.Services.Configure<AuthApiSettings>(builder.Configuration.GetSection("AuthApiSettings"));

builder.Services.AddSingleton(RestClient.For<IUserApi>("https://localhost:7228/api/users"));
builder.Services.AddSingleton(RestClient.For<IAuthApi>("https://localhost:7228/api/auth"));
builder.Services.AddSingleton(RestClient.For<IProductApi>("https://localhost:7262/api/products"));

builder.Services.AddSingleton<IConnectionMultiplexer>(
    sp => ConnectionMultiplexer.Connect("localhost:6379") 
);

builder.Services.AddHttpContextAccessor();

var eventBuilder =  builder.AddRabbitMqEventBus("localhost");

eventBuilder.AddSubscription<CustomerCreatedIntegrationEvent, CustomerCreatedIntegrationEventHandler>();
eventBuilder.AddSubscription<StockUpdatedIntegrationEvent, StockUpdatedIntegrationEventHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapOrderEndpoints()
    .MapCustomerEndpoints();

app.Lifetime.ApplicationStarted.Register(async () =>
{
    using var scope = app.Services.CreateScope();
    var productService = scope.ServiceProvider.GetRequiredService<IProductStockService>();
    await productService.PopulateProductStocksDbAsync();
});

app.Run();

