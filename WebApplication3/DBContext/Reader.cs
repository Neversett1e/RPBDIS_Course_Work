using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public partial class Reader
    {
        public Reader()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }

        public int ReaderId { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassportInfo { get; set; }

        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
