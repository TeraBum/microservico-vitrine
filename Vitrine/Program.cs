using Microsoft.EntityFrameworkCore;
using Vitrine.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com PostgreSQL (Supabase)
builder.Services.AddDbContext<VitrineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa Controllers
app.MapControllers();

app.Run();
