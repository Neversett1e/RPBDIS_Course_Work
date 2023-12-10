using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class BorrowedBook
    {
        public int BorrowId { get; set; }
        [Display(Name = "Книга")]
        public int? BookId { get; set; }
        [Display(Name = "Читатель")]
        public int? ReaderId { get; set; }
        [Display(Name = "Дата выдачи книги")]
        public DateTime? BorrowDate { get; set; }
        [Display(Name = "Дата возврата книги")]
        public DateTime? ReturnDate { get; set; }
        [Display(Name = "Отметка о возврате")]
        public bool? Returned { get; set; }
        [Display(Name = "Сотрудник")]
        public int? EmployeeId { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Reader? Reader { get; set; }
    }
}
