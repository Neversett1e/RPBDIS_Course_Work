using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    public class ReadersController : Controller
    {
        private readonly LibraryDBContext _context;

        public ReadersController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: Readers
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(
    int page = 1,
    string fullName = null,
    DateTime? dateOfBirth = null,
    string gender = null,
    string address = null,
    string phoneNumber = null,
    string passportInfo = null,
    ReadersSortState sortOrder = ReadersSortState.FullNameAsc
)
        {
            IQueryable<Reader> readers = _context.Readers;

            if (!string.IsNullOrEmpty(fullName))
            {
                readers = readers.Where(r => r.FullName.Contains(fullName));
            }

            if (dateOfBirth.HasValue)
            {
                readers = readers.Where(r => r.DateOfBirth == dateOfBirth);
            }

            if (!string.IsNullOrEmpty(gender))
            {
                readers = readers.Where(r => r.Gender == gender);
            }

            if (!string.IsNullOrEmpty(address))
            {
                readers = readers.Where(r => r.Address.Contains(address));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                readers = readers.Where(r => r.PhoneNumber.Contains(phoneNumber));
            }

            if (!string.IsNullOrEmpty(passportInfo))
            {
                readers = readers.Where(r => r.PassportInfo.Contains(passportInfo));
            }

            int pageSize = 10;

            var count = await readers.CountAsync();
            var items = await readers
                .OrderBy(r => r.FullName) // Default sorting
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            switch (sortOrder)
            {
                case ReadersSortState.FullNameDesc:
                    items = items.OrderByDescending(r => r.FullName).ToList();
                    break;
                case ReadersSortState.FullNameAsc:
                    items = items.OrderBy(r => r.FullName).ToList();
                    break;
                case ReadersSortState.DateOfBirthDesc:
                    items = items.OrderByDescending(r => r.DateOfBirth).ToList();
                    break;
                case ReadersSortState.DateOfBirthAsc:
                    items = items.OrderBy(r => r.DateOfBirth).ToList();
                    break;
                default:
                    items = items.OrderBy(r => r.FullName).ToList();
                    break;
            }

            var filterViewModel = new ReadersFilterViewModel(fullName, dateOfBirth, gender, address, phoneNumber, passportInfo);
            var sortViewModel = new ReadersSortViewModel(sortOrder);
            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new PaginationViewModel<Reader, ReadersFilterViewModel, ReadersSortViewModel>(
                items, pageViewModel, filterViewModel, sortViewModel
            );

            return View(viewModel);
        }


        // GET: Readers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .FirstOrDefaultAsync(m => m.ReaderId == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: Readers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ReaderId,FullName,DateOfBirth,Gender,Address,PhoneNumber,PassportInfo")] Reader reader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: Readers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ReaderId,FullName,DateOfBirth,Gender,Address,PhoneNumber,PassportInfo")] Reader reader)
        {
            if (id != reader.ReaderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaderExists(reader.ReaderId))
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
            return View(reader);
        }

        // GET: Readers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers
                .FirstOrDefaultAsync(m => m.ReaderId == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Readers == null)
            {
                return Problem("Entity set 'LibraryDBContext.Readers'  is null.");
            }
            var reader = await _context.Readers.FindAsync(id);
            if (reader != null)
            {
                _context.Readers.Remove(reader);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaderExists(int id)
        {
          return (_context.Readers?.Any(e => e.ReaderId == id)).GetValueOrDefault();
        }
    }
}
