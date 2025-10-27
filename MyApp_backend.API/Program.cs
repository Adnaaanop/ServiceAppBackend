using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApp_backend.Application.DTOs.User;
using MyApp_backend.Application.Helpers;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Application.Mapping;
using MyApp_backend.Application.Services;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Interfaces;
using MyApp_backend.Infrastructure.Data;

using MyApp_backend.Infrastructure.Repositories;
using MyApp_backend.Infrastructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with Identity support
builder.Services.AddDbContext<MyAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity with ApplicationUser and Guid as key
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<MyAppDbContext>()
.AddDefaultTokenProviders();

// Configure JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; // Change to false in development if needed
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Register application services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtTokenHelper>();


//Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//Services
builder.Services.AddScoped<IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, ApplicationUser>, UserService>();
//Automapper
builder.Services.AddAutoMapper(typeof(UserProfile));


// Swagger configuration: Enable JWT "Authorize" button and lock icons
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApp API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
