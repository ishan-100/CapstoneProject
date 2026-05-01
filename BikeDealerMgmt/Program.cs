using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BikeDealerMgmtAPI.Models;
using BikeDealerMgmtAPI.Services.Implementations;
using BikeDealerMgmtAPI.Services.Interfaces;
using BikeDealerMgmtAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<BikeDealerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Services
builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<IDealerService, DealerService>();
builder.Services.AddScoped<IDealerMasterService, DealerMasterService>();

// JWT
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new Exception("JWT Secret missing");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
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
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Swagger with JWT Bearer support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BikeDealerMgmtAPI",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token. Example: Bearer eyJhbGci..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// REMOVED: app.UseHttpsRedirection()
// Reason: Gateway routes to HTTP (http://localhost:5219).
// HttpsRedirection would cause 307 redirects breaking internal calls.

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();