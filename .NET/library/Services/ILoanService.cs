using OneBeyondApi.Model;

namespace OneBeyondApi.Services
{
    public interface ILoanService
    {
        Task<IEnumerable<BorrowerWithLoanedBooks>> GetLoans();
        BookStock BorrowerReturnOneBookStock(ReturnBookRequest returnBookRequest);
    }
}