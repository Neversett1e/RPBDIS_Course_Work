using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public partial class Book
    {
        public Book()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }

        public int BookId { get; set; }
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? PublisherId { get; set; }
        public int? PublicationYear { get; set; }
        public int? GenreId { get; set; }
        public decimal? Price { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
