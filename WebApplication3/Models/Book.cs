using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class Book
    {
        public Book()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }

        public int BookId { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}-\d{7}$", ErrorMessage = "Формат ISBN должен быть 123-4567890")]
        [Display(Name = "ISBN")]
        public string? Isbn { get; set; }
        [Display(Name = "Название книги")]
        public string? Title { get; set; }
        [Display(Name = "Автор")]
        public string? Author { get; set; }
        [Display(Name = "Издатель")]
        public int? PublisherId { get; set; }
        [Required]
        [Range(1, 2023, ErrorMessage = "Значение должно быть в пределах от 1 до 2023")]
        [Display(Name = "Год издания")]
        public int? PublicationYear { get; set; }
        [Display(Name = "Жанр")]
        public int? GenreId { get; set; }
        [Display(Name = "Цена")]
        public decimal? Price { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
