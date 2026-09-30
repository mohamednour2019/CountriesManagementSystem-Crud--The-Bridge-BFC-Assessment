namespace CountriesManagementSystem.Shared.SharedDTOs
{
    public class SearchListDto
    {
        public Paginator Paginator { get; set; }
    }

    public class Paginator
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
