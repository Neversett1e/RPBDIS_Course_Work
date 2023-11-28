using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public partial class Employee
    {
        public Employee()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }

        public int EmployeeId { get; set; }
        public string FullName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
