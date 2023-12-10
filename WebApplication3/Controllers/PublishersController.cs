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
    public class PublishersController : Controller
    {
        private readonly LibraryDBContext _context;

        public PublishersController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: Publishers
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(
    int page = 1,
    string name = null,
    string city = null,
    string address = null,
    PublisherSortState sortOrder = PublisherSortState.NameAsc
)
        {
            IQueryable<Publisher> publishers = _context.Publishers;

            if (!string.IsNullOrEmpty(name))
            {
                publishers = publishers.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(city))
            {
                publishers = publishers.Where(p => p.City.Contains(city));
            }

            if (!string.IsNullOrEmpty(address))
            {
                publishers = publishers.Where(p => p.Address.Contains(address));
            }

            int pageSize = 10;

            var count = await publishers.CountAsync();
            var items = await publishers
                .OrderBy(p => p.Name) // Default sorting
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            switch (sortOrder)
            {
                case PublisherSortState.NameDesc:
                    items = items.OrderByDescending(p => p.Name).ToList();
                    break;
                case PublisherSortState.NameAsc:
                    items = items.OrderBy(p => p.Name).ToList();
                    break;
                case PublisherSortState.CityDesc:
                    items = items.OrderByDescending(p => p.City).ToList();
                    break;
                case PublisherSortState.CityAsc:
                    items = items.OrderBy(p => p.City).ToList();
                    break;
                case PublisherSortState.AddressDesc:
                    items = items.OrderByDescending(p => p.Address).ToList();
                    break;
                case PublisherSortState.AddressAsc:
                    items = items.OrderBy(p => p.Address).ToList();
                    break;
                default:
                    items = items.OrderBy(p => p.Name).ToList();
                    break;
            }


            var filterViewModel = new PublisherFilterViewModel(name, city, address);
            var sortViewModel = new PublisherSortViewModel(sortOrder);
            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new PaginationViewModel<Publisher, PublisherFilterViewModel, PublisherSortViewModel>(
                items, pageViewModel, filterViewModel, sortViewModel
            );

            return View(viewModel);
        }


        // GET: Publishers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("PublisherId,Name,City,Address")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Name,City,Address")] Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.PublisherId))
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
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publishers == null)
            {
                return Problem("Entity set 'LibraryDBContext.Publishers'  is null.");
            }
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
          return (_context.Publishers?.Any(e => e.PublisherId == id)).GetValueOrDefault();
        }
    }
}
