using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication3
{
    public enum BorrowedBookSortState
    {
        BorrowDateAsc, BorrowDateDesc,
        ReturnDateAsc, ReturnDateDesc,
        ReturnedAsc, ReturnedDesc,
        EmployeeAsc, EmployeeDesc,
        ReaderAsc, ReaderDesc,
        BookTitleAsc, BookTitleDesc
    }

    public class BorrowedBookSortViewModel
    {
        public BorrowedBookSortState BorrowDateSort { get; }
        public BorrowedBookSortState ReturnDateSort { get; }
        public BorrowedBookSortState ReturnedSort { get; }
        public BorrowedBookSortState EmployeeSort { get; }
        public BorrowedBookSortState ReaderSort { get; }
        public BorrowedBookSortState Current { get; }
        public BorrowedBookSortState BookTitleSort { get; }

        public BorrowedBookSortViewModel(BorrowedBookSortState state)
        {
            BorrowDateSort = state == BorrowedBookSortState.BorrowDateAsc ? BorrowedBookSortState.BorrowDateDesc : BorrowedBookSortState.BorrowDateAsc;
            ReturnDateSort = state == BorrowedBookSortState.ReturnDateAsc ? BorrowedBookSortState.ReturnDateDesc : BorrowedBookSortState.ReturnDateAsc;
            ReturnedSort = state == BorrowedBookSortState.ReturnedAsc ? BorrowedBookSortState.ReturnedDesc : BorrowedBookSortState.ReturnedAsc;
            EmployeeSort = state == BorrowedBookSortState.EmployeeAsc ? BorrowedBookSortState.EmployeeDesc : BorrowedBookSortState.EmployeeAsc;
            ReaderSort = state == BorrowedBookSortState.ReaderAsc ? BorrowedBookSortState.ReaderDesc : BorrowedBookSortState.ReaderAsc;
            BookTitleSort = state == BorrowedBookSortState.BookTitleAsc ? BorrowedBookSortState.BookTitleDesc : BorrowedBookSortState.BookTitleAsc;

            Current = state;
        }
    }
}

