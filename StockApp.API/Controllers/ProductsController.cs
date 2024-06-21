using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.API.Resources;
using StockApp.Application.Services;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ProductImportService _productImportService;

        public ProductsController(IProductRepository productRepository, IStringLocalizer<SharedResource> localizer, ProductImportService productImportService)
        {
            _productRepository = productRepository;
            _localizer = localizer;
            _productImportService = productImportService;
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos.</returns>

        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Obtém um produto específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>O produto correspondente ao ID.</returns>

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

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="product">O produto a ser criado.</param>
        /// <returns>O produto criado.</returns>

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

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="product">Os dados atualizados do produto.</param>
        /// <returns>No content.</returns>

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

        /// <summary>
        /// Deleta um produto específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do produto a ser deletado.</param>
        /// <returns>No content.</returns>

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

        /// <summary>
        /// Atualiza vários produtos em massa.
        /// </summary>
        /// <param name="products">A lista de produtos a serem atualizados.</param>
        /// <returns>No content.</returns>

        [HttpPut("bulk-update")]
        public async Task<ActionResult> BulkUpdate([FromBody] List<Product> products)
        {
            await _productRepository.BulkUpdateAsync(products);
            return NoContent();
        }

        /// <summary>
        /// Busca produtos com base em critérios de consulta.
        /// </summary>
        /// <param name="query">A consulta de busca.</param>
        /// <param name="sortBy">Campo de ordenação.</param>
        /// <param name="descending">Se a ordenação deve ser descendente.</param>
        /// <returns>Lista de produtos que correspondem aos critérios de busca.</returns>

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> Search (
            [FromQuery] string query, 
            [FromQuery] string sortBy, 
            [FromQuery] bool descending)
        {
            var products = await _productRepository.SearchAsync(query, sortBy, descending);
            return Ok(products);
        }

        /// <summary>
        /// Exporta os produtos para um arquivo CSV.
        /// </summary>
        /// <returns>Arquivo CSV contendo todos os produtos.</returns>

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

        /// <summary>
        /// Obtém produtos filtrados por nome e intervalo de preço.
        /// </summary>
        /// <param name="name">Nome do produto.</param>
        /// <param name="minPrice">Preço mínimo.</param>
        /// <param name="maxPrice">Preço máximo.</param>
        /// <returns>Lista de produtos filtrados.</returns>

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFiltered([FromQuery] string name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var products = await _productRepository.GetFilteredAsync(name, minPrice, maxPrice);
            return Ok(products);
        }

        /// <summary>
        /// Faz upload de uma imagem para um produto específico.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="image">Arquivo de imagem a ser carregado.</param>
        /// <returns>Status da operação.</returns>

        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Invalid image.");
            }

            var filePath = System.IO.Path.Combine("wwwroot/images", $"{id}.jpg");

            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Ok();
        }

        /// <summary>
        /// Importa produtos de um arquivo CSV.
        /// </summary>
        /// <param name="file">Arquivo CSV contendo os produtos.</param>
        /// <returns>Status da operação.</returns>

        [HttpPost("import")]
        public async Task<IActionResult> ImportFromCsv(IFormFile file)
        {
            if (file == null || file.Length == 0) 
            {
                return BadRequest("Invalid file.");
            }

            await _productImportService.ImportProductAsync(file.OpenReadStream());

            return Ok();
        }
    }
}
