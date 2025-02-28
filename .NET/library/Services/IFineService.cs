using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public interface IFineService
{
    public Task<Fine> AddFineForBorrower(DateTime loanEndDate, Borrower borrower);
}