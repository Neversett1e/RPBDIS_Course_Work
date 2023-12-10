using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class BooksOnLoan
    {
        public int BookId { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}-\d{7}$", ErrorMessage = "Формат ISBN должен быть 123-4567890")]
        public string? Isbn { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? PublisherId { get; set; }
        [Required]
        [Range(1, 2023, ErrorMessage = "Значение должно быть в пределах от 1 до 2023")]
        public int? PublicationYear { get; set; }
        public int? GenreId { get; set; }
        public decimal? Price { get; set; }
        public int? ReaderId { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? Returned { get; set; }
    }
}
