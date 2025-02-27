using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public class LoanService : ILoanService
{
    private readonly ICatalogueRepository _catalogueRepository;

    public LoanService(ICatalogueRepository catalogueRepository)
    {
        _catalogueRepository = catalogueRepository;
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

    public Guid RemoveLoan(BookStock bookStock)
    {
        throw new NotImplementedException();
    }
}