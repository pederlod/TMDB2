namespace TMDB2.Models
{
    public class SeriesList
    {
        public List<Series>? Series { get; set; }
        public int? TotalCount { get; set; }

        // properties for pagination

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

