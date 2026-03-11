using LupoQuimica.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder; // Adicione se não tiver
using Microsoft.AspNetCore.Hosting; // Adicione se não tiver
// Versao do Deploy: 2.0
var builder = WebApplication.CreateBuilder(args);

// Tenta pegar a conexão da variável do Railway, se não tiver, usa a fixa (fallback)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=pZmUPChWtJOCXmigsMPJSfFGfVkRiugx";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => {
    options.AddPolicy("LupoPolicy", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Proteção para não quebrar se a chave JWT não estiver configurada
var jwtKey = builder.Configuration["Jwt:Key"] ?? "Chave_Mestra_Temporaria_De_32_Caracteres_!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "LupoQuimica";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "LupoQuimica";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });


var app = builder.Build();

// Migração automática (O "coração" do deploy)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}


// Swagger fora do IF para podermos ver no Railway
app.UseSwagger();
app.UseSwaggerUI();

//app.UseBlazorFrameworkFiles(); // Essencial para Blazor WASM
app.UseDefaultFiles();
app.MapStaticAssets();



app.UseCors("LupoPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html"); // Isso garante que o Front carregue na raiz

app.Run();