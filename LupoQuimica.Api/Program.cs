using LupoQuimica.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// String de Conexão para SQL Server Local (LocalDB)
// O banco será criado automaticamente com o nome LupoQuimicaDB
var connectionString = "Server=bppc490\\SQLEXPRESS;Database=LupoQuimicaDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)); // Mudamos para UseSqlServer

builder.Services.AddControllers();

// No .NET 10, o Swagger básico é ativado assim:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de CORS para o Blazor
builder.Services.AddCors(options => {
    options.AddPolicy("LupoPolicy", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
var app = builder.Build();
// Ativa a interface visual do Swagger para você testar a API no navegador
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("LupoPolicy");
app.UseAuthentication(); // <-- Quem é você?
app.UseAuthorization();  // <-- O que você pode fazer?
app.MapControllers();

app.Run();