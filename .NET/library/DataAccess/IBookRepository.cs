using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBookRepository
    {
        public List<Book> GetBooks();

        public Guid AddBook(Book book);

        public Task<Book> GetBookByTitle(string title);

        public Book UpdateBook(Book book);
    }
}
