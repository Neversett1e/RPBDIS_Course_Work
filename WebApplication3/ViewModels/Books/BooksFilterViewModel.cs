using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication3
{
    public class BooksFilterViewModel
    {
        public string Isbn { get; }
        public string Title { get; }
        public string Author { get; }
        public int? PublisherId { get; }
        public int? GenreId { get; }
        public decimal? Price { get; }

        public SelectList Publishers { get; }
        public SelectList Genres { get; }

        public BooksFilterViewModel(List<Publisher> publishers, List<Genre> genres, string isbn, string title, string author, int? publisherId, int? genreId, decimal? price)
        {
            publishers.Insert(0, new Publisher { PublisherId = 0, Name = "Все" });
            genres.Insert(0, new Genre { GenreId = 0, Name = "Все" });

            Publishers = new SelectList(publishers, "PublisherId", "Name");
            Genres = new SelectList(genres, "GenreId", "Name");

            Isbn = isbn;
            Title = title;
            Author = author;
            PublisherId = publisherId;
            GenreId = genreId;
            Price = price;
        }
    }
}


