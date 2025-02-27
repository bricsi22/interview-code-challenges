namespace OneBeyondApi.Model;

public class BorrowerWithLoanedBooks : Borrower
{
    public IEnumerable<string> BooksOnLoan { get; set; }
}