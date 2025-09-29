using Microsoft.EntityFrameworkCore;
using Vitrine.Data;
using Vitrine.Seed; // <-- Importa a SeedData

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

// Popula dados iniciais (Seed)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VitrineDbContext>();
    // Aplica migrações pendentes automaticamente
    dbContext.Database.Migrate();
    // Executa seed
    SeedData.Initialize(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa Controllers
app.MapControllers();

app.Run();