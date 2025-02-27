namespace OneBeyondApi.Model
{
    public class CatalogueSearch
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public bool? ActiveLoansOnly { get; set; }
        public bool? HasBorrower { get; set; }
    }
}
