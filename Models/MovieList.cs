namespace TMDB2.Models
{
    public class MovieList
    {
        public List<Movie>? Movies { get; set; }

        // For eventual pagination
        public int? TotalCount { get; set; }

        // Add these properties for pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}