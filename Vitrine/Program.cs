using Microsoft.EntityFrameworkCore;
using Vitrine.Data; // namespace do DbContext

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com PostgreSQL
builder.Services.AddDbContext<VitrineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger no dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint exemplo: listar produtos
app.MapGet("/products", async (VitrineDbContext db) =>
    await db.Products.ToListAsync()
)
.WithName("GetProducts")
.WithOpenApi();

app.Run();
