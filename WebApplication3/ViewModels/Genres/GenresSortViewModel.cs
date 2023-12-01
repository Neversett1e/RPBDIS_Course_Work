namespace WebApplication3
{
    public enum GenresSortState
    {
        NameAsc, NameDesc,
        DescriptionAsc, DescriptionDesc
    }

    public class GenresSortViewModel
    {
        public GenresSortState NameSort { get; }
        public GenresSortState DescriptionSort { get; }
        public GenresSortState Current { get; }

        public GenresSortViewModel(GenresSortState state)
        {
            NameSort = state == GenresSortState.NameAsc ? GenresSortState.NameDesc : GenresSortState.NameAsc;
            DescriptionSort = state == GenresSortState.DescriptionAsc ? GenresSortState.DescriptionDesc : GenresSortState.DescriptionAsc;

            Current = state;
        }
    }
}

