using GM.ShopFlow.Product.Api.Endpoints;
using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Application.UseCases.Category.CreateCategory;
using GM.ShopFlow.Product.Application.UseCases.Category.GetCategories;
using GM.ShopFlow.Product.Application.UseCases.Product.CreateProduct;
using GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;
using GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;
using GM.ShopFlow.Product.Application.UseCases.Stock.RegisterProductStock;
using GM.ShopFlow.Product.Application.UseCases.Stock.SupplyStock;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Product.Infra.Data;
using GM.ShopFlow.Product.Infra.Data.Context;
using GM.ShopFlow.Product.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GM.ShopFlow.Product.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Product.Application.IntegrationsEvents.EventHandlers;
using GM.ShopFlow.Shared.EventBus.Extensions;
using GM.ShopFlow.Shared.EventBusRabbitMQ;

internal class Program
{
    private static void Main(string[] args)
    {
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

        builder.Services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
        );

        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateProduct).Assembly));
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IStockRepository, StockRepository>();
        builder.Services.AddScoped<ICreateCategory, CreateCategory>();
        builder.Services.AddScoped<IGetCategories, GetCategories>();
        builder.Services.AddScoped<ICreateProduct, CreateProduct>();
        builder.Services.AddScoped<IGetProductById, GetProductById>();
        builder.Services.AddScoped<IGetProducts, GetProducts>();
        builder.Services.AddScoped<IRegisterProductStock, RegisterProductStock>();
        builder.Services.AddScoped<ISupplyStock, SupplyStock>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        var eventBuilder = builder.AddRabbitMqEventBus("localhost");

        eventBuilder.AddSubscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.MapCategoryEndpoints()
            .MapProductEndpoints()
            .MapStockEndpoints();

        app.Run();
    }
}