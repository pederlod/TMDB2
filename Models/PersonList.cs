namespace TMDB2.Models
{
    public class PersonList
    {
        public List<Person>? People { get; set; }
        public int? TotalCount { get; set; }

        // properties for pagination

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
