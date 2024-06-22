using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeePerformanceEvaluationService evaluationService;

        public EmployeeController(IEmployeePerformanceEvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        [HttpGet("evaluate/{employeeId}")]
        public async Task<ActionResult<EmployeeEvaluationDto>> EvaluatePerformance(int employeeId)
        {
            var evaluation = await evaluationService.EvaluatePerformanceAsync(employeeId);
            if (evaluation == null)
            {
                return NotFound("Employee not found");
            }
            return Ok(evaluation);
        }

    }
}
