using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface ICatalogueRepository
    {
        public List<BookStock> GetCatalogue();
        public BookStock GetBookStock(string bookStockId);
        public Task<BookStock> GetBookStockByBookTitle(string bookTitle);
        public Task<BookStock> GetBookStockByBookId(Guid bookId);
        public Task<IEnumerable<BookStock>> SearchCatalogue(CatalogueSearch search);
        public BookStock BorrowerReturnOneBookStock(BookStock bookStock);
    }
}
