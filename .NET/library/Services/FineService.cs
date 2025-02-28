using OneBeyondApi.Constans;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public class FineService: IFineService
{
    private readonly IFineRepository _fineRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public FineService(
        IFineRepository fineRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _fineRepository = fineRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Fine> AddFineForBorrower(DateTime loanEndDate, Borrower borrower)
    {
        decimal fineAmount = CalculateFineAmount(loanEndDate);
        var fine = new Fine
        {
            Amount = fineAmount,
            Borrower = borrower
        };

        return await _fineRepository.AddFine(fine);
    }

    private decimal CalculateFineAmount(DateTime loanEndDate)
    {
        var numberOfDays = _dateTimeProvider.GetCurrentUtcDate().Subtract(loanEndDate).Days;
        return numberOfDays * Constant.DefaultFineAmount;
    }
}