namespace OneBeyondApi.Model;

public class Fine
{
    public Guid Id { get; set; }
    public Borrower Borrower { get; set; }
    public decimal Amount { get; set; }
}