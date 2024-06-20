using Microsoft.AspNetCore.Mvc;
using StockApp.Domain;
using StockApp.Domain.Interfaces;


namespace StockApp.API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpGet(Name = "GetSuppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            if (suppliers == null)
            {
                return NotFound("Suppliers not found");
            }
            return Ok(suppliers);
        }

        [HttpGet("{id: int}", Name = "GetSupplier")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }
            return Ok(supplier);
        }

        [HttpPost( Name = "CreateSupplier")]
        public async Task<ActionResult> Post([FromBody] Supplier supplier)
        {
            if (supplier == null)
            {
                return BadRequest("Invalid data");
            }
            await _supplierRepository.AddAsync(supplier);

            return new CreatedAtRouteResult("GetSupplier", new { id = supplier.Id }, supplier);
        }

        [HttpPut("{id:int}", Name = "UpdateSupplier")]
        public async Task<ActionResult> Put(int id,[FromBody] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest("Inconsistent Id");
            }
            if (supplier == null)
            {
                return BadRequest("Invalid data");
            }

            await _supplierRepository.UpdateAsync(supplier);

            return Ok(supplier);
        }

        [HttpDelete("{id:int}", Name = "DeleteSupplier")]
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }

            await _supplierRepository.DeleteAsync(id);

            return Ok(supplier);
        }

        [HttpGet("search", Name = "SearchSuppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> Search([FromQuery] string name, [FromQuery] string contactEmail)
        {
            var suppliers = await _supplierRepository.SearchAsync(name, contactEmail);
            if (suppliers == null || !suppliers.Any())
            {
                return NotFound("No suppliers found with the given criteria");
            }
            return Ok(suppliers);
        }
    }
}
