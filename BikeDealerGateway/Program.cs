using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Load configs
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// JWT — must match IdentityWebAPI exactly
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new Exception("JWT Secret missing");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

// CORS — for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Must be last — Ocelot handles all routing
await app.UseOcelot();

app.Run();