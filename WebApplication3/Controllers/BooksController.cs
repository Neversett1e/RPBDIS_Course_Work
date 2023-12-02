using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3
{
    public class BooksController : Controller
    {
        private readonly LibraryDBContext _context;

        public BooksController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: Books
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, string isbn = null, string title = null, string author = null, int publisherId = 0, int genreId = 0, decimal? price = null, SortState sortOrder = SortState.TitleAsc, int publicationYear = 0)
        {
            IQueryable<Book> books = _context.Books.Include(b => b.Genre).Include(b => b.Publisher);

            if (!string.IsNullOrEmpty(isbn))
            {
                books = books.Where(b => b.Isbn.Contains(isbn));
            }

            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books = books.Where(b => b.Author.Contains(author));
            }

            if (publisherId != 0)
            {
                books = books.Where(b => b.PublisherId == publisherId);
            }

            if (genreId != 0)
            {
                books = books.Where(b => b.GenreId == genreId);
            }

            if (price.HasValue)
            {
                books = books.Where(b => b.Price == price);
            }

            if (publicationYear != 0)
            {
                books = books.Where(b => b.PublicationYear == publicationYear);
            }

            int pageSize = 10;

            var count = await books.CountAsync();
            var items = await books.OrderBy(b => b.BookId)
                                  .Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            switch (sortOrder)
            {
                case SortState.IsbnDesc:
                    items = items.OrderByDescending(b => b.Isbn).ToList();
                    break;
                case SortState.TitleDesc:
                    items = items.OrderByDescending(b => b.Title).ToList();
                    break;
                case SortState.AuthorDesc:
                    items = items.OrderByDescending(b => b.Author).ToList();
                    break;
                case SortState.PublisherDesc:
                    items = items.OrderByDescending(b => b.Publisher.Name).ToList();
                    break;
                case SortState.GenreDesc:
                    items = items.OrderByDescending(b => b.Genre.Name).ToList();
                    break;
                case SortState.PriceDesc:
                    items = items.OrderByDescending(b => b.Price).ToList();
                    break;
                case SortState.PublicationYearDesc:
                    items = items.OrderByDescending(b => b.PublicationYear).ToList();
                    break;
                default:
                    items = items.OrderBy(b => b.Title).ToList();
                    break;
            }

            var filterViewModel = new BooksFilterViewModel(
                _context.Publishers.ToList(),
                _context.Genres.ToList(),
                isbn, title, author, publisherId, genreId, price, publicationYear
            );

            var sortViewModel = new BooksSortViewModel(sortOrder);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            PaginationViewModel<Book, BooksFilterViewModel, BooksSortViewModel> viewModel =
                new PaginationViewModel<Book, BooksFilterViewModel, BooksSortViewModel>(items, pageViewModel, filterViewModel, sortViewModel);

            return View(viewModel);
        }


        // GET: Books/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("BookId,Isbn,Title,Author,PublisherId,PublicationYear,GenreId,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.PublisherId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Isbn,Title,Author,PublisherId,PublicationYear,GenreId,Price")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", book.GenreId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.PublisherId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'LibraryDBContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
