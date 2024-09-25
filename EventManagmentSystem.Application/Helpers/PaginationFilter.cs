namespace EventManagmentSystem.Application.Helpers
{
    public class PaginationFilter
    {

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;


        public string SortBy { get; set; } = ""; // Default sort by property

        public string SortOrder { get; set; } = "asc"; // "asc" or "desc"

        public string? SearchQuery { get; set; } // Optional search query
    }

}

