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
    public class BorrowedBooksController : Controller
    {
        private readonly LibraryDBContext _context;

        public BorrowedBooksController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooks
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(
    int page = 1,
    DateTime? borrowDate = null,
    DateTime? returnDate = null,
    bool? returned = null,
    int employeeId = 0,
    int readerId = 0,
    BorrowedBookSortState sortOrder = BorrowedBookSortState.BorrowDateAsc
            )
        {
            IQueryable<BorrowedBook> borrowedBooks = _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.Employee)
                .Include(b => b.Reader);

            if (borrowDate.HasValue)
            {
                borrowedBooks = borrowedBooks.Where(b => b.BorrowDate == borrowDate);
            }

            if (returnDate.HasValue)
            {
                borrowedBooks = borrowedBooks.Where(b => b.ReturnDate == returnDate);
            }

            if (returned.HasValue)
            {
                borrowedBooks = borrowedBooks.Where(b => b.Returned == returned);
            }

            if (employeeId != 0)
            {
                borrowedBooks = borrowedBooks.Where(b => b.EmployeeId == employeeId);
            }

            if (readerId != 0)
            {
                borrowedBooks = borrowedBooks.Where(b => b.ReaderId == readerId);
            }

            int pageSize = 10;

            var count = await borrowedBooks.CountAsync();
            var items = await borrowedBooks
                .OrderBy(b => b.BorrowDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            switch (sortOrder)
            {
                case BorrowedBookSortState.BorrowDateDesc:
                    items = items.OrderByDescending(b => b.BorrowDate).ToList();
                    break;
                case BorrowedBookSortState.ReturnDateDesc:
                    items = items.OrderByDescending(b => b.ReturnDate).ToList();
                    break;
                case BorrowedBookSortState.ReturnedDesc:
                    items = items.OrderByDescending(b => b.Returned).ToList();
                    break;
                case BorrowedBookSortState.EmployeeDesc:
                    items = items.OrderByDescending(b => b.Employee.FullName).ToList();
                    break;
                case BorrowedBookSortState.ReaderDesc:
                    items = items.OrderByDescending(b => b.Reader.FullName).ToList();
                    break;
                default:
                    items = items.OrderBy(b => b.BorrowDate).ToList();
                    break;
            }

            var filterViewModel = new BorrowedBookFilterViewModel(
                _context.Employees.ToList(),
                _context.Readers.ToList(),
                borrowDate, returnDate, returned, employeeId, readerId
            );

            var sortViewModel = new BorrowedBookSortViewModel(sortOrder);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            PaginationViewModel<BorrowedBook, BorrowedBookFilterViewModel, BorrowedBookSortViewModel> viewModel =
                new PaginationViewModel<BorrowedBook, BorrowedBookFilterViewModel, BorrowedBookSortViewModel>(items, pageViewModel, filterViewModel, sortViewModel);

            return View(viewModel);
        }


        // GET: BorrowedBooks/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BorrowedBooks == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.Employee)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.BorrowId == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ReaderId", "ReaderId");
            return View();
        }

        // POST: BorrowedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("BorrowId,BookId,ReaderId,BorrowDate,ReturnDate,Returned,EmployeeId")] BorrowedBook borrowedBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", borrowedBook.BookId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", borrowedBook.EmployeeId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ReaderId", "ReaderId", borrowedBook.ReaderId);
            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BorrowedBooks == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", borrowedBook.BookId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", borrowedBook.EmployeeId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ReaderId", "ReaderId", borrowedBook.ReaderId);
            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowId,BookId,ReaderId,BorrowDate,ReturnDate,Returned,EmployeeId")] BorrowedBook borrowedBook)
        {
            if (id != borrowedBook.BorrowId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedBookExists(borrowedBook.BorrowId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", borrowedBook.BookId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", borrowedBook.EmployeeId);
            ViewData["ReaderId"] = new SelectList(_context.Readers, "ReaderId", "ReaderId", borrowedBook.ReaderId);
            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BorrowedBooks == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.Employee)
                .Include(b => b.Reader)
                .FirstOrDefaultAsync(m => m.BorrowId == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BorrowedBooks == null)
            {
                return Problem("Entity set 'LibraryDBContext.BorrowedBooks'  is null.");
            }
            var borrowedBook = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBook != null)
            {
                _context.BorrowedBooks.Remove(borrowedBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBookExists(int id)
        {
          return (_context.BorrowedBooks?.Any(e => e.BorrowId == id)).GetValueOrDefault();
        }
    }
}
