using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public partial class BooksOnLoan
    {
        public int BookId { get; set; }
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? PublisherId { get; set; }
        public int? PublicationYear { get; set; }
        public int? GenreId { get; set; }
        public decimal? Price { get; set; }
        public int? ReaderId { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? Returned { get; set; }
    }
}
