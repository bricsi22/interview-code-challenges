using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface ICatalogueRepository
    {
        public List<BookStock> GetCatalogue();
        public BookStock GetBookStock(string bookStockId);
        public Task<IEnumerable<BookStock>> SearchCatalogue(CatalogueSearch search);
        public BookStock BorrowerReturnOneBookStock(BookStock bookStock);
    }
}
