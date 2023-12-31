﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3
{
    public class GenresController : Controller
    {
        private readonly LibraryDBContext _context;

        public GenresController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: Genres
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1, string name = null, string description = null, GenresSortState sortOrder = GenresSortState.NameAsc)
        {
            IQueryable<Genre> genres = _context.Genres;

            if (!string.IsNullOrEmpty(name))
            {
                genres = genres.Where(g => g.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(description))
            {
                genres = genres.Where(g => g.Description.Contains(description));
            }

            int pageSize = 10;

            var count = await genres.CountAsync();
            var items = await genres.OrderBy(g => g.GenreId)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            switch (sortOrder)
            {
                case GenresSortState.NameDesc:
                    items = items.OrderByDescending(g => g.Name).ToList();
                    break;
                case GenresSortState.NameAsc:
                    items = items.OrderByDescending(g => g.Name).Reverse().ToList();
                    break;
                case GenresSortState.DescriptionDesc:
                    items = items.OrderByDescending(g => g.Description).ToList();
                    break;
                case GenresSortState.DescriptionAsc:
                    items = items.OrderByDescending(g => g.Description).Reverse().ToList();
                    break;
                default:
                    items = items.OrderBy(g => g.Name).ToList();
                    break;
            }

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            var filterViewModel = new GenresFilterViewModel(name, description);
            var sortViewModel = new GenresSortViewModel(sortOrder);

            PaginationViewModel<Genre, GenresFilterViewModel, GenresSortViewModel> viewModel =
                new PaginationViewModel<Genre, GenresFilterViewModel, GenresSortViewModel>(items, pageViewModel, filterViewModel, sortViewModel);

            return View(viewModel);
        }


        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("GenreId,Name,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("GenreId,Name,Description")] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreId))
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
            return View(genre);
        }

        // GET: Genres/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'LibraryDBContext.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
          return (_context.Genres?.Any(e => e.GenreId == id)).GetValueOrDefault();
        }
    }
}
