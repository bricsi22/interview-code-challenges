
namespace OneBeyondApi.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentUtcDate()
    {
        return DateTime.UtcNow;
    }
}