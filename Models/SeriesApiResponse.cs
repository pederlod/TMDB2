namespace TMDB2.Models
{
    public class SeriesApiResponse
    {
        public int? Page { get; set; }
        public List<Series>? Results { get; set; }
        public int? TotalResults { get; set; }
        public int? TotalPages { get; set; }
    }
}
