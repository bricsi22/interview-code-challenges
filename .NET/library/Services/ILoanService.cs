using OneBeyondApi.Model;

namespace OneBeyondApi.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<BorrowerWithLoanedBooks>> GetLoans();
        Guid RemoveLoan(BookStock bookStock);
    }
}