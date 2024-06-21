using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    public class SupplierRelationshipManagementServiceController : ControllerBase
    {
        private readonly ISupplierRelationshipManagementService _supplierRelationshipManagementService;

        public SupplierRelationshipManagementServiceController(ISupplierRelationshipManagementService supplierRelationshipManagementService)
        {
            _supplierRelationshipManagementService = supplierRelationshipManagementService;
        }

        [HttpGet("evaluate({supplierId}")]
        public async Task<ActionResult<SupplierDto>> EvaluateSupplier(int supplierId)
        {
            var supplier = await _supplierRelationshipManagementService.EvaluateSupplierAsync(supplierId);
            return Ok(supplier);
        }

        [HttpGet("renew({supplierId}")]
        public async Task<ActionResult<SupplierDto>> RenewContract(int supplierId)
        {
            var result = await _supplierRelationshipManagementService.RenewContractAsync(supplierId);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
