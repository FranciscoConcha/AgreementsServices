using src.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using src.services.interfaces;
using src.services;
using src.utils;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddOpenApi();
builder.Services.AddControllers();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Port=5432;Database=card_services;Username=postgres;Password=password";
builder.Services.AddDbContext<CardDbContext>(options =>
    options.UseNpgsql(connectionString));

var jwtKey = builder.Configuration["Jwt:key"] ??  throw new InvalidOperationException("JWT key no configurado");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ??  throw new InvalidOperationException("Jwt Issuer no configurad");
var jwtAudience = builder.Configuration["Jwt:Audience"] ??  throw new InvalidOperationException("JWT audience no configurado");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new     TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer =jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
builder.Services.AddAuthentication();
builder.Services.AddScoped<IAuthServices,AuthServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<ICardServices, CardServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<JwtUtils>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
