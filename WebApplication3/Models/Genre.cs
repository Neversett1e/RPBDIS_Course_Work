using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int GenreId { get; set; }
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
