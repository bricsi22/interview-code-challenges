using System.ComponentModel.DataAnnotations;

namespace OneBeyondApi.Model
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public BookFormat Format { get; set; }
        public string ISBN { get; set; }
        public bool Preserved { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
