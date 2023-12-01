namespace WebApplication3
{
    public enum SortState
    {
        IsbnAsc, IsbnDesc,
        TitleAsc, TitleDesc,
        AuthorAsc, AuthorDesc,
        PublisherAsc, PublisherDesc,
        GenreAsc, GenreDesc,
        PriceAsc, PriceDesc
    }

    public class BooksSortViewModel
    {
        public SortState IsbnSort { get; }
        public SortState TitleSort { get; }
        public SortState AuthorSort { get; }
        public SortState PublisherSort { get; }
        public SortState GenreSort { get; }
        public SortState PriceSort { get; }
        public SortState Current { get; }

        public BooksSortViewModel(SortState state)
        {
            IsbnSort = state == SortState.IsbnAsc ? SortState.IsbnDesc : SortState.IsbnAsc;
            TitleSort = state == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            AuthorSort = state == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            PublisherSort = state == SortState.PublisherAsc ? SortState.PublisherDesc : SortState.PublisherAsc;
            GenreSort = state == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            PriceSort = state == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;

            Current = state;
        }
    }
}



