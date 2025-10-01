using Microsoft.EntityFrameworkCore;
using Vitrine.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com PostgreSQL (Supabase)
builder.Services.AddDbContext<VitrineDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure();
            npgsqlOptions.CommandTimeout(120); // Timeout maior
        }
    )
);

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Popula a tabela com SeedData em batches de 5
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VitrineDbContext>();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Usa Controllers
app.MapControllers();

app.Run();