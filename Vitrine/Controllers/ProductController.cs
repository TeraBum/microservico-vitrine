using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vitrine.Models;

namespace Vitrine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly VitrineDbContext _context;

        public ProductController(VitrineDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            int page = 1,
            int pageSize = 10,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? sortBy = null,
            string? sortOrder = "asc")
        {
            var query = _context.Products.AsQueryable();

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

            return Ok(new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Products = products
            });
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // GET: api/Product/{id}/stock
        [HttpGet("{id}/stock")]
        public async Task<IActionResult> GetProductStock(Guid id)
        {
            var stockItems = await _context.StockItems
                .Where(s => s.ProductId == id)
                .Include(s => s.StockMoves)
                .ToListAsync();

            if (!stockItems.Any()) return NotFound();

            return Ok(stockItems);
        }
    }
}