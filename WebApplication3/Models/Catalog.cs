using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public partial class Catalog
    {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int? PublicationYear { get; set; }
        public string? Genre { get; set; }
        public decimal? Price { get; set; }
    }
}
