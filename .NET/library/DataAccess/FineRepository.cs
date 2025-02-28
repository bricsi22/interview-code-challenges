using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public class FineRepository : IFineRepository
{
    public List<Fine> GetFines()
    {
        using (var context = new LibraryContext())
        {
            var list = context.Fines
                .Include(x => x.Borrower)
                .ToList();

            return list;
        }
    }

    public async Task<Fine> AddFine(Fine fine)
    {
        using (var context = new LibraryContext())
        {
            context.Fines.Add(fine);
            await context.SaveChangesAsync();
            return fine;
        }
    }
}