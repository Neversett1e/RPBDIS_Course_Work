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
    public class EmployeesController : Controller
    {
        private readonly LibraryDBContext _context;

        public EmployeesController(LibraryDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(
    string fullName,
    string position,
    string phoneNumber,
    string address,
    EmployeeSortState sortOrder = EmployeeSortState.FullNameAsc
)
        {
            IQueryable<Employee> employees = _context.Employees;

            if (!string.IsNullOrEmpty(fullName))
            {
                employees = employees.Where(e => e.FullName.Contains(fullName));
            }

            if (!string.IsNullOrEmpty(position))
            {
                employees = employees.Where(e => e.Position.Contains(position));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                employees = employees.Where(e => e.PhoneNumber.Contains(phoneNumber));
            }

            if (!string.IsNullOrEmpty(address))
            {
                employees = employees.Where(e => e.Address.Contains(address));
            }

            int pageSize = 10;

            var count = await employees.CountAsync();
            var items = await employees
                .OrderByDescending(e => e.FullName) // Assuming default sorting is descending by full name
                .Take(pageSize)
                .ToListAsync();

            switch (sortOrder)
            {
                case EmployeeSortState.FullNameAsc:
                    items = items.OrderBy(e => e.FullName).ToList();
                    break;
                case EmployeeSortState.FullNameDesc:
                    items = items.OrderByDescending(e => e.FullName).ToList();
                    break;
                case EmployeeSortState.PositionAsc:
                    items = items.OrderBy(e => e.Position).ToList();
                    break;
                case EmployeeSortState.PositionDesc:
                    items = items.OrderByDescending(e => e.Position).ToList();
                    break;
                case EmployeeSortState.PhoneNumberAsc:
                    items = items.OrderBy(e => e.PhoneNumber).ToList();
                    break;
                case EmployeeSortState.PhoneNumberDesc:
                    items = items.OrderByDescending(e => e.PhoneNumber).ToList();
                    break;
                case EmployeeSortState.AddressAsc:
                    items = items.OrderBy(e => e.Address).ToList();
                    break;
                case EmployeeSortState.AddressDesc:
                    items = items.OrderByDescending(e => e.Address).ToList();
                    break;
                default:
                    items = items.OrderByDescending(e => e.FullName).ToList();
                    break;
            }


            var filterViewModel = new EmployeeFilterViewModel(fullName, position, phoneNumber, address);
            var sortViewModel = new EmployeeSortViewModel(sortOrder);
            var pageViewModel = new PageViewModel(count, 1, pageSize);

            var viewModel = new PaginationViewModel<Employee, EmployeeFilterViewModel, EmployeeSortViewModel>(
                items, pageViewModel, filterViewModel, sortViewModel
            );

            return View(viewModel);
        }


        // GET: Employees/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("EmployeeId,FullName,Position,PhoneNumber,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FullName,Position,PhoneNumber,Address")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'LibraryDBContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
          return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
