using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        [Display(Name = "Название")]
        public string? Name { get; set; }
        [Display(Name = "Город")]
        public string? City { get; set; }
        [Display(Name = "Адресс")]
        public string? Address { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
