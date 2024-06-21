using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Linq;
using System.Text;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository; 

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new {id = product.Id}, product);
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("bulk-update")]
        public async Task<ActionResult> BulkUpdate([FromBody] List<Product> products)
        {
            await _productRepository.BulkUpdateAsync(products);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search (
            [FromQuery] string query, 
            [FromQuery] string sortBy, 
            [FromQuery] bool descending)
        {
            var products = await _productRepository.SearchAsync(query, sortBy, descending);
            return Ok(products);
        }

        [HttpGet("export-csv")]
        public async Task<ActionResult<IEnumerable<Product>>> ExportToCsv()
        {
            var products = await _productRepository.GetAllAsync();
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Description,Price,Stock");

            foreach (var product in products)
            {
                csv.AppendLine($"{product.Id},{product.Name},{product.Description},{product.Price},{product.Stock}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "products.csv");
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFiltered([FromQuery] string name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var products = await _productRepository.GetFilteredAsync(name, minPrice, maxPrice);
            return Ok(products);
        }

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Invalid image.");
            }

            var filePath = Path.Combine("wwwroot/images", $"{id}.jpg");

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Ok();
        }
    }
}
