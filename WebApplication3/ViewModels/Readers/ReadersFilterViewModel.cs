using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace WebApplication3
{
    public class ReadersFilterViewModel
    {
        public string FullName { get; }
        public DateTime? DateOfBirth { get; }
        public string Gender { get; }
        public string Address { get; }
        public string PhoneNumber { get; }
        public string PassportInfo { get; }

        public ReadersFilterViewModel(string fullName, DateTime? dateOfBirth, string gender, string address, string phoneNumber, string passportInfo)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            PassportInfo = passportInfo;
        }
    }
}

