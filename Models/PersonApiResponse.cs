namespace TMDB2.Models
{
    public class PersonApiResponse
    {
        public int? Page { get; set; }
        public List<Person>? Results { get; set; }
        public int? TotalResults { get; set; }
        public int? TotalPages { get; set; }
    }
}
