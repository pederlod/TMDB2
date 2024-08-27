namespace TMDB2.Models
{
    public class MovieApiResponse
    {
        public int? Page { get; set; }
        public List<Movie>? Results { get; set; }
        public int? TotalResults { get; set; }
        public int? TotalPages { get; set; }
    }
}
