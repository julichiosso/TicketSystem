using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using TicketSystem.API.Middleware;
using TicketSystem.Aplicacion.Interfaces;
using TicketSystem.Aplicacion.Servicios;
using TicketSystem.Dominio.Entidades;
using TicketSystem.Infraestructura.Datos;
using TicketSystem.Infraestructura.Repositorios;
using TicketSystem.Infraestructura.Seed;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Controllers + JSON + FluentValidation
// --------------------
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddFluentValidationAutoValidation();

// --------------------
// DbContext
// --------------------
builder.Services.AddDbContext<TicketSystemDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------
// Repositorios
// --------------------
builder.Services.AddScoped<IRepositorioTickets, RepositorioTickets>();
builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();

// --------------------
// Servicios
// --------------------
builder.Services.AddScoped<IServicioTickets, ServicioTickets>();
builder.Services.AddScoped<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

// --------------------
// JWT Configuration
// --------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = ClaimTypes.Role,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// --------------------
// Swagger + JWT Support
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese SOLO el token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// --------------------
// Build
// --------------------
var app = builder.Build();

// --------------------
// Middleware global de errores
// --------------------
app.UseMiddleware<ExceptionMiddleware>();

// --------------------
// Swagger (solo en desarrollo)
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔐 IMPORTANTE: primero Authentication, después Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// --------------------
// Seed automático
// --------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TicketSystemDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<Usuario>>();

    await DataSeeder.SeedAsync(context, passwordHasher);
}

app.Run();
