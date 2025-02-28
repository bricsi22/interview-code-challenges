using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;
using OneBeyondApi.Validators;

namespace OneBeyondApi.Services;

public class LoanService : ILoanService
{
    private readonly ICatalogueRepository _catalogueRepository;
    private readonly ReturnBookValidator _returnBookValidator;
    private readonly IFineService _fineService;

    public LoanService(
        ICatalogueRepository catalogueRepository,
        ReturnBookValidator returnBookValidator,
        IFineService fineService)
    {
        _catalogueRepository = catalogueRepository;
        _returnBookValidator = returnBookValidator;
        _fineService = fineService;
    }

    public async Task<IEnumerable<BorrowerWithLoanedBooks>> GetLoans()
    {
        var catalogueSearch = new CatalogueSearch
        {
            ActiveLoansOnly = true,
            HasBorrower = true
        };

        var catalogueResults = await _catalogueRepository.SearchCatalogue(catalogueSearch);
        var result = new List<BorrowerWithLoanedBooks>();
        foreach (var catalogueResult in catalogueResults.GroupBy(x => x.OnLoanTo))
        {
            var listOfBooks = catalogueResult.Select(x => x.Book.Name);
            result.Add(new BorrowerWithLoanedBooks
            {
                Id = catalogueResult.Key.Id,
                Name = catalogueResult.Key.Name,
                EmailAddress = catalogueResult.Key.EmailAddress,
                BooksOnLoan = listOfBooks
            });
        }

        return result;
    }

    public BookStock BorrowerReturnOneBookStock(ReturnBookRequest returnBookRequest)
    {
        var bookStock = _catalogueRepository.GetBookStock(returnBookRequest.BookStockId);
        var returnBookValidationResult = _returnBookValidator.Validate(bookStock);
        if (returnBookValidationResult.IsValid)
        {
            return _catalogueRepository.BorrowerReturnOneBookStock(bookStock);
        }
        else
        {
            if (bookStock.LoanEndDate.HasValue && bookStock.OnLoanTo != null)
            {
                _fineService.AddFineForBorrower(bookStock.LoanEndDate.Value, bookStock.OnLoanTo);
            }
        }

        return bookStock;
    }
}