using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public interface IReserveBookService
{
    public Task<ReserveBookResponse> ReserveBook(ReserveBookRequest reserveBookRequest);
}