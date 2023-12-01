namespace WebApplication3
{
    public class EmployeeFilterViewModel
    {
        public string FullName { get; }
        public string Position { get; }
        public string PhoneNumber { get; }
        public string Address { get; }

        public EmployeeFilterViewModel(string fullName, string position, string phoneNumber, string address)
        {
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}


