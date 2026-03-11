using LupoQuimica.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// String de Conexão para SQL Server Local (LocalDB)
// O banco será criado automaticamente com o nome LupoQuimicaDB
var connectionString = "Server=switchback.proxy.rlwy.net,23800;Database=LupoQuimicaDb;User Id=sa;Password=SUA_SENHA_AQUI;TrustServerCertificate=True";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)); // Mudamos para Postgre

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Isso cria as tabelas no Railway automaticamente
}

// Ativa a interface visual do Swagger para você testar a API no navegador
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("LupoPolicy");
app.UseAuthentication(); // <-- Quem é você?
app.UseAuthorization();  // <-- O que você pode fazer?
app.MapControllers();

app.Run();