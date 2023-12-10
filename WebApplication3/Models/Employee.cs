using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class Employee
    {
        public Employee()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }

        public int EmployeeId { get; set; }
        [Required]
        [RegularExpression(@"^\w+\s+\w+\s+\w+$", ErrorMessage = "Формат строки должен быть 'Фамилия + Имя + Отчество'")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; } = null!;
        [Display(Name = "Должность")]
        public string Position { get; set; } = null!;
        [Required]
        [RegularExpression(@"^\+375\d{9}$", ErrorMessage = "Формат строки должен быть +375*********")]
        [Display(Name = "Номер телефона")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Аресс")]
        public string? Address { get; set; }

        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
