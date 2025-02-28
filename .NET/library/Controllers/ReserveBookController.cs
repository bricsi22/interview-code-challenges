using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Services;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReserveBookController : ControllerBase
    {
        private readonly ILogger<ReserveBookController> _logger;
        private readonly IReserveBookService _reserveBookService;

        public ReserveBookController(
            ILogger<ReserveBookController> logger,
            IReserveBookService reserveBookService)
        {
            _logger = logger;
            _reserveBookService = reserveBookService;
        }

        [HttpPost]
        [Route("ReserveBook")]
        public async Task<ReserveBookResponse> ReserveBook(ReserveBookRequest reserveBookRequest)
        {
            _logger.LogDebug("Reserve endpoint called.");
            return await _reserveBookService.ReserveBook(reserveBookRequest);
        }
    }
}