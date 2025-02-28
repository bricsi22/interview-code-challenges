using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BookRepository : IBookRepository
    {
        public BookRepository()
        {
        }
        public List<Book> GetBooks()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Books
                    .ToList();
                return list;
            }
        }
        public async Task<Book> GetBookByTitle(string title)
        {
            using (var context = new LibraryContext())
            {
                var book = await context.Books
                    .Where(b => b.Title == title)
                    .SingleOrDefaultAsync();
                return book;
            }
        }

        public Guid AddBook(Book book)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
                return book.Id;
            }
        }

        public Book UpdateBook(Book book)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Update(book);
                context.SaveChanges();
                return book;
            }
        }
    }
}
