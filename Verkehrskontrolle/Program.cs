using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Options;
using Verkehrskontrolle.Services;

var builder = WebApplication.CreateBuilder(args);

// Create jwtSettings and bind to appsettins.json
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(JwtSettings.SectionName, jwtSettings);
builder.Services.AddSingleton(Options.Create(jwtSettings));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application services to the container.
builder.Services.AddTransient<IHalterAbfrageService, HalterAbfrageService>();
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddDbContext<VerkehrskontrolleDbContext>((opt) =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddDbContext<AuthDbContext>((opt) =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"),cfg =>
    {
        cfg.MigrationsHistoryTable("__EFMigrationsHistory_Auth");
    });
});

// Register and setup authentication service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
    });


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

app.MapControllers();

app.Run();
