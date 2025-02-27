using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.Model;
using OneBeyondApi.Services;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService _loanService;

        public LoanController(
            ILogger<LoanController> logger,
            ILoanService loanService)
        {
            _logger = logger;
            _loanService = loanService;
        }

        [HttpGet]
        [Route("OnLoan")]
        public async Task<IEnumerable<BorrowerWithLoanedBooks>> GetLoans()
        {
            _logger.LogDebug("Loan endpoint called.");
            return await _loanService.GetLoans();
        }

        [HttpPost]
        [Route("ReturnBook")]
        public Guid ReturnBook(BookStock bookStock)
        {
            return _loanService.RemoveLoan(bookStock);
        }
    }
}