namespace TMDB2.Models
{
    public class MovieList
    {
        public List<Movie>? Movies { get; set; }

        // For eventual pagination
        public int? TotalCount { get; set; } 
        
    }
}
