using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3
{
    public partial class Reader
    {
        public Reader()
        {
            BorrowedBooks = new HashSet<BorrowedBook>();
        }

        public int ReaderId { get; set; }
        [Required]
        [RegularExpression(@"^\w+\s+\w+\s+\w+$", ErrorMessage = "Формат строки должен быть 'Фамилия + Имя + Отчество'")]
        [Display(Name = "ФИО")]
        public string? FullName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [RegularExpression("[МЖ]", ErrorMessage = "Строка должна содержать только букву 'М' или 'Ж'")]
        [Display(Name = "Пол")]
        public string? Gender { get; set; }
        [Display(Name = "Адресс")]
        public string? Address { get; set; }
        [Required]
        [RegularExpression(@"^\+375\d{9}$", ErrorMessage = "Формат строки должен быть +375*********")]
        [Display(Name = "Номер телефона")]
        public string? PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^KB\d{7}$", ErrorMessage = "Формат строки должен быть 'KB1234567'")]
        [Display(Name = "Номер пасспорта")]
        public string? PassportInfo { get; set; }

        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
