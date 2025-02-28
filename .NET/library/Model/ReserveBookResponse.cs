namespace OneBeyondApi.Model;

public class ReserveBookResponse
{
    public bool BookReserved { get; set; }

    public DateTime? NextTimeAvailable { get; set; }
}