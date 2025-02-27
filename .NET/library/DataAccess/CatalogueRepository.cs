using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;
using OneBeyondApi.Services;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CatalogueRepository(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public List<BookStock> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .ToList();
                return list;
            }
        }

        public async Task<IEnumerable<BookStock>> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.OnLoanTo)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author)) {
                        list = list.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }

                    if (!string.IsNullOrEmpty(search.BookName)) {
                        list = list.Where(x => x.Book.Name.Contains(search.BookName));
                    }

                    if (search.ActiveLoansOnly.HasValue && search.ActiveLoansOnly.Value)
                    {
                        var currentDate = _dateTimeProvider.GetCurrentUtcDate();
                        list = list.Where(x => x.LoanEndDate.HasValue == false || currentDate <= x.LoanEndDate.Value);
                    }

                    if (search.HasBorrower.HasValue && search.HasBorrower.Value)
                    {
                        list = list.Where(x => x.OnLoanTo != null);
                    }
                }

                return await list.ToListAsync();
            }
        }

        public BookStock BorrowerReturnOneBookStock(ReturnBookRequest returnBookRequest)
        {

            using (var context = new LibraryContext())
            {
                var bookStockGuid = new Guid(returnBookRequest.BookStockId);
                var catalogue = context.Catalogue.SingleOrDefault(x => x.Id == bookStockGuid);

                if (catalogue != null)
                {
                    catalogue.OnLoanTo = null;
                    catalogue.LoanEndDate = null;
                    return context.Update(catalogue).Entity;
                }
            }

            return null;
        }
    }
}
