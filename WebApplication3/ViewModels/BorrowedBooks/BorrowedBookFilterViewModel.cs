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

        public BorrowedBookFilterViewModel(List<Employee> employees, List<Reader> readers, DateTime? borrowDate, DateTime? returnDate, bool? returned, int employeeId, int readerId)
        {
            employees.Insert(0, new Employee { EmployeeId = 0, FullName = "Все" });
            readers.Insert(0, new Reader { ReaderId = 0, FullName = "Все" });

            Employees = new SelectList(employees, "EmployeeId", "EmployeeName");
            Readers = new SelectList(readers, "ReaderId", "ReaderName");

            BorrowDate = borrowDate;
            ReturnDate = returnDate;
            Returned = returned;
            EmployeeId = employeeId;
            ReaderId = readerId;
        }
    }
}

