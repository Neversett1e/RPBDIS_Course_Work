using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication3
{
    public enum EmployeeSortState
    {
        FullNameAsc, FullNameDesc,
        PositionAsc, PositionDesc,
        PhoneNumberAsc, PhoneNumberDesc,
        AddressAsc, AddressDesc
    }

    public class EmployeeSortViewModel
    {
        public EmployeeSortState FullNameSort { get; }
        public EmployeeSortState PositionSort { get; }
        public EmployeeSortState PhoneNumberSort { get; }
        public EmployeeSortState AddressSort { get; }

        public EmployeeSortState Current { get; }

        public EmployeeSortViewModel(EmployeeSortState state)
        {
            FullNameSort = state == EmployeeSortState.FullNameAsc ? EmployeeSortState.FullNameDesc : EmployeeSortState.FullNameAsc;
            PositionSort = state == EmployeeSortState.PositionAsc ? EmployeeSortState.PositionDesc : EmployeeSortState.PositionAsc;
            PhoneNumberSort = state == EmployeeSortState.PhoneNumberAsc ? EmployeeSortState.PhoneNumberDesc : EmployeeSortState.PhoneNumberAsc;
            AddressSort = state == EmployeeSortState.AddressAsc ? EmployeeSortState.AddressDesc : EmployeeSortState.AddressAsc;

            Current = state;
        }
    }
}
