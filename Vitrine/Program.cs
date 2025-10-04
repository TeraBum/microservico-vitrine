using Microsoft.EntityFrameworkCore;
using Vitrine.Models;

var builder = WebApplication.CreateBuilder(args);

// Lê variáveis de ambiente diretamente
var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "aws-1-sa-east-1.pooler.supabase.com";
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432"; // Shared Pooler
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres.smjdaavxsnbmrdrvejsu";
var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "S3nhaS3gur@:(";

// Monta connection string para Shared Pooler
var connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password};Ssl Mode=Require;Trust Server Certificate=true";

// Configura o DbContext com PostgreSQL (Supabase Shared Pooler)
builder.Services.AddDbContext<VitrineDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(); // Agora funciona no Shared Pooler
            npgsqlOptions.CommandTimeout(120);    // Timeout maior
        }
    )
);

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Inicializa contexto (opcional)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VitrineDbContext>();
    // SeedData.Initialize(context); // Se quiser popular a tabela
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();