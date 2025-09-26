using Microsoft.EntityFrameworkCore;
using Vitrine.Data;
using Vitrine.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com PostgreSQL (Supabase)
builder.Services.AddDbContext<VitrineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// ===== Endpoints Vitrine (apenas leitura) =====

// Listar produtos com filtros, paginação e ordenação
app.MapGet("/products", async (VitrineDbContext db,
                                int page = 1,
                                int pageSize = 10,
                                string? category = null,
                                decimal? minPrice = null,
                                decimal? maxPrice = null,
                                string? sortBy = null,
                                string? sortOrder = "asc") =>
{
    var query = db.Products.AsQueryable();

    // Filtros
    if (!string.IsNullOrEmpty(category))
        query = query.Where(p => p.Category == category);

    if (minPrice.HasValue)
        query = query.Where(p => p.Price >= minPrice.Value);

    if (maxPrice.HasValue)
        query = query.Where(p => p.Price <= maxPrice.Value);

    // Ordenação
    query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
    {
        ("price", "desc") => query.OrderByDescending(p => p.Price),
        ("price", "asc") => query.OrderBy(p => p.Price),
        ("name", "desc") => query.OrderByDescending(p => p.Name),
        ("name", "asc") => query.OrderBy(p => p.Name),
        _ => query.OrderBy(p => p.CreatedAt)
    };

    // Paginação
    var totalItems = await query.CountAsync();
    var products = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return Results.Ok(new
    {
        TotalItems = totalItems,
        Page = page,
        PageSize = pageSize,
        Products = products
    });
})
.WithName("GetProducts")
.WithOpenApi();

// Retornar detalhes de produto
app.MapGet("/products/{id}", async (Guid id, VitrineDbContext db) =>
{
    var product = await db.Products.FindAsync(id);
    return product != null ? Results.Ok(product) : Results.NotFound();
})
.WithName("GetProductById")
.WithOpenApi();

// Endpoint de teste de conexão
app.MapGet("/test", async (VitrineDbContext db) =>
{
    try
    {
        var count = await db.Products.CountAsync();
        return Results.Ok($"Conexão OK! Produtos cadastrados: {count}");
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Erro na conexão: {ex.Message}");
    }
})
.WithName("TestConnection")
.WithOpenApi();

app.Run();
