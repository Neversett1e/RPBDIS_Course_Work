namespace WebApplication3
{
    public enum ReadersSortState
    {
        FullNameAsc, FullNameDesc,
        DateOfBirthAsc, DateOfBirthDesc,
        GenderAsc, GenderDesc,
        AddressAsc, AddressDesc,
        PhoneNumberAsc, PhoneNumberDesc,
        PassportInfoAsc, PassportInfoDesc
    }

    public class ReadersSortViewModel
    {
        public ReadersSortState FullNameSort { get; }
        public ReadersSortState DateOfBirthSort { get; }
        public ReadersSortState GenderSort { get; }
        public ReadersSortState AddressSort { get; }
        public ReadersSortState PhoneNumberSort { get; }
        public ReadersSortState PassportInfoSort { get; }
        public ReadersSortState Current { get; }

        public ReadersSortViewModel(ReadersSortState state)
        {
            FullNameSort = state == ReadersSortState.FullNameAsc ? ReadersSortState.FullNameDesc : ReadersSortState.FullNameAsc;
            DateOfBirthSort = state == ReadersSortState.DateOfBirthAsc ? ReadersSortState.DateOfBirthDesc : ReadersSortState.DateOfBirthAsc;
            GenderSort = state == ReadersSortState.GenderAsc ? ReadersSortState.GenderDesc : ReadersSortState.GenderAsc;
            AddressSort = state == ReadersSortState.AddressAsc ? ReadersSortState.AddressDesc : ReadersSortState.AddressAsc;
            PhoneNumberSort = state == ReadersSortState.PhoneNumberAsc ? ReadersSortState.PhoneNumberDesc : ReadersSortState.PhoneNumberAsc;
            PassportInfoSort = state == ReadersSortState.PassportInfoAsc ? ReadersSortState.PassportInfoDesc : ReadersSortState.PassportInfoAsc;

            Current = state;
        }
    }
}


