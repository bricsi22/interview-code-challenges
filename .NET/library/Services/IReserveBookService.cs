using OneBeyondApi.Model;

namespace OneBeyondApi.Services;

public interface IReserveBookService
{
    public Task<bool> ReserveBook(ReserveBookRequest reserveBookRequest);
}