using Bookstore.Helpers;

namespace Bookstore.ViewModels
{
    public class SortViewModel
    {
        public SortState TitleSort { get; private set; }
        public SortState AuthorNameSort { get; private set; }
        public SortState GenreSort { get; private set; }
        public SortState PriceSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            AuthorNameSort = sortOrder == SortState.AuthorNameAsc ? SortState.AuthorNameDesc : SortState.AuthorNameAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
