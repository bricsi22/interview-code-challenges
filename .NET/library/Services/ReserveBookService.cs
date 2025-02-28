using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public class ReserveBookService : IReserveBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<ReserveBookService> _logger;
    private readonly ICatalogueRepository _catalogueRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReserveBookService(
        IBookRepository bookRepository,
        ILogger<ReserveBookService> logger,
        ICatalogueRepository catalogueRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _bookRepository = bookRepository;
        _logger = logger;
        _catalogueRepository = catalogueRepository;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<ReserveBookResponse> ReserveBook(ReserveBookRequest reserveBookRequest)
    {
        var bookStock = await _catalogueRepository.GetBookStockByBookTitle(reserveBookRequest.BookTitle);
        var reserveBookResponse = new ReserveBookResponse
        {
            BookReserved = false
        };

        if (bookStock != null)
        {
            if (bookStock.LoanEndDate.HasValue)
            {
                if (bookStock.Book.Preserved == false)
                {
                    try
                    {
                        bookStock.Book.Preserved = true;
                        _bookRepository.UpdateBook(bookStock.Book);
                        reserveBookResponse.BookReserved = true;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        _logger.LogError("Book already modified by someone else, cannot reserve it.");
                        reserveBookResponse.BookReserved = false;
                    }
                }
                

                if (reserveBookResponse.BookReserved == false)
                {
                    if (bookStock.LoanEndDate.Value < _dateTimeProvider.GetCurrentUtcDate())
                    {
                        reserveBookResponse.NextTimeAvailable = _dateTimeProvider.GetCurrentUtcDate();
                    }
                    else
                    {
                        reserveBookResponse.NextTimeAvailable = bookStock.LoanEndDate.Value.AddDays(1);
                    }
                }
            }

        }

        return reserveBookResponse;
    }
}