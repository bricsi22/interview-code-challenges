using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface ICatalogueRepository
    {
        public List<BookStock> GetCatalogue();

        public Task<IEnumerable<BookStock>> SearchCatalogue(CatalogueSearch search);
    }
}
