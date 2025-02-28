using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public class ReserveBookService : IReserveBookService
{
    private readonly IBookRepository _bookRepository;

    public ReserveBookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<bool> ReserveBook(ReserveBookRequest reserveBookRequest)
    {
        var book = await _bookRepository.GetBookByTitle(reserveBookRequest.BookTitle);
        if (book != null && book.Preserved == false)
        {
            bool reserveSuccess;
            try
            {
                book.Preserved = true;
                _bookRepository.UpdateBook(book);
                reserveSuccess = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                reserveSuccess = false;
            }

            return reserveSuccess;
        }

        return false;
    }
}