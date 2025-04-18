using GM.ShopFlow.Identity.Context;
using GM.ShopFlow.Identity.Endpoints;
using GM.ShopFlow.Identity.Models;
using GM.ShopFlow.Identity.Services.Auth;
using GM.ShopFlow.Identity.Services.Token;
using GM.ShopFlow.Identity.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopFlowDbContext>(options => 
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ShopFlowDbContext>()
    .AddDefaultTokenProviders();

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

app
    .MapAuthEndpoints()
    .MapUserEndpoints()
    .MapRoleEndpoints();

app.Run();

