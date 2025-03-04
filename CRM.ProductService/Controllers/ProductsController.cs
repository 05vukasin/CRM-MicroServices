using CRM.ProductService.Data;
using CRM.ProductService.DTOs;
using CRM.ProductService.Models;
using CRM.ProductService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ImageService _imageService; // Dodato za upload slika

        public ProductsController(DataContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] PostProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                SKU = product.SKU,
                Category = product.Category,
                UpdatedAt = DateTime.UtcNow,
                IsActive = product.IsActive,
                Supplier = product.Supplier
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadProductImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Fajl nije poslat.");
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Ako proizvod već ima sliku, prvo je brišemo iz Blob Storage-a
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                await _imageService.DeleteImageAsync(Path.GetFileName(product.ImageUrl));
            }

            // Upload nove slike u Blob Storage
            using var stream = file.OpenReadStream();
            string imageUrl = await _imageService.UploadImageAsync(file.FileName, stream);

            // Ažuriramo proizvod sa novom slikom
            product.ImageUrl = imageUrl;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Slika uspešno otpremljena.", ImageUrl = imageUrl });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] PostProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.SKU = product.SKU;
            existingProduct.Category = product.Category;
            existingProduct.UpdatedAt = DateTime.UtcNow;
            existingProduct.IsActive = product.IsActive;
            existingProduct.Supplier = product.Supplier;

            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

    }
}
