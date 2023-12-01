using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
