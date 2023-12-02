using WebApplication3;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WebApplication3
{
    public class BorrowedBookFilterViewModel
    {
        public SelectList Employees { get; }
        public SelectList Readers { get; }
        public DateTime? BorrowDate { get; }
        public DateTime? ReturnDate { get; }
        public bool? Returned { get; }
        public int EmployeeId { get; }
        public int ReaderId { get; }
        public string BookTitle { get; }

        public BorrowedBookFilterViewModel(List<Employee> employees, List<Reader> readers, DateTime? borrowDate, DateTime? returnDate, bool? returned, int employeeId, int readerId, string bookTitle)
        {
            employees.Insert(0, new Employee { EmployeeId = 0, FullName = "Все" });
            readers.Insert(0, new Reader { ReaderId = 0, FullName = "Все" });

            Employees = new SelectList(employees, "EmployeeId", "FullName");
            Readers = new SelectList(readers, "ReaderId", "FullName");

            Returned = returned;
            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            EmployeeId = employeeId;
            ReaderId = readerId;
            BookTitle = bookTitle;
        }
    }
}

