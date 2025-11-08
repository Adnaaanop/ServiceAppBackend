using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApp_backend.API.Hubs;
using MyApp_backend.Application.DTOs.Booking;
using MyApp_backend.Application.DTOs.Catalog;
using MyApp_backend.Application.DTOs.Message;
using MyApp_backend.Application.DTOs.Payment;
using MyApp_backend.Application.DTOs.User;
using MyApp_backend.Application.Helpers;
using MyApp_backend.Application.Interfaces;
using MyApp_backend.Application.Mapping;
using MyApp_backend.Application.Services;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Entities.Catalog;
using MyApp_backend.Domain.Entities.Payment;
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
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IWarrantyRequestRepository, WarrantyRequestRepository>();
builder.Services.AddScoped<IEmergencyAlertRepository, EmergencyAlertRepository>();

// Add this to your DI section with other services
builder.Services.AddScoped<IGenericRepository<Message>, GenericRepository<Message>>(); // For Message entity
builder.Services.AddScoped<IGenericService<MessageCreateDto, object, MessageResponseDto, Message>, MessageService>();

//Services
builder.Services.AddScoped<IProviderService, ProviderService>();
builder.Services.AddScoped<IGenericService<UserRequestDto, UserUpdateDto, UserResponseDto, ApplicationUser>, UserService>();
builder.Services.AddScoped<IGenericService<ServiceCategoryCreateDto, ServiceCategoryUpdateDto, ServiceCategoryResponseDto, ServiceCategory>, ServiceCategoryService>();
builder.Services.AddScoped<IGenericService<ServiceCreateDto, ServiceUpdateDto, ServiceResponseDto, Service>, ServiceService>();
builder.Services.AddScoped<IGenericService<BookingCreateDto, BookingUpdateDto, BookingResponseDto, Booking>, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentService, RazorpayPaymentService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IGenericService<PricingRuleCreateDto, PricingRuleUpdateDto, PricingRuleResponseDto, PricingRule>, PricingRuleService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IWarrantyRequestService, WarrantyRequestService>();
builder.Services.AddScoped<IEmergencyAlertService, EmergencyAlertService>();

// Add this in your Program.cs before building the app
builder.Services.Configure<MyApp_backend.Application.Settings.RazorpaySettings>(builder.Configuration.GetSection("Razorpay"));


//Automapper
builder.Services.AddAutoMapper(
    typeof(UserProfile),
    typeof(ProviderProfileMapping),
    typeof(CatalogProfile),
    typeof(BookingProfile),
    typeof(ReviewProfile),
    typeof(PaymentProfile),
    typeof(PricingRuleProfile),
    typeof(MessageProfile),
    typeof(WarrantySafetyProfile)
 );


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        b => b.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});


builder.Services.AddSignalR();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication();




app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");
app.MapHub<EmergencyAlertHub>("/emergencyAlertHub");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    string[] roles = new[] { "Admin", "Provider", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role, NormalizedName = role.ToUpper() });
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    // 1. Make sure "Admin" role exists (you may already have this)
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole<Guid> { Name = "Admin", NormalizedName = "ADMIN" });

    // 2. Create a unique GUID for admin user
    var adminEmail = "Admin0@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            Name = "Super Admin",
            IsActive = true,
            IsVerified = true,
            CreatedAt = DateTime.UtcNow
            // add other fields needed for your model
        };
        var result = await userManager.CreateAsync(newAdmin, "Admin0@gmail.com"); // change password if needed
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
            Console.WriteLine("Admin user created");
        }
        else
        {
            Console.WriteLine("Failed admin create: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
    else
    {
        // Ensure user is in admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            await userManager.AddToRoleAsync(adminUser, "Admin");
        Console.WriteLine("Admin user already exists");
    }
}

app.Run();
