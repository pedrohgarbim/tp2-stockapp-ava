using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetRecommendations(string userId)
        {
            var recommendations = await _recommendationService.GetRecommendationsAsync(userId);
            return Ok(recommendations);
        }
    }
}
