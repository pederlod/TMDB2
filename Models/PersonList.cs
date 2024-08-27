namespace TMDB2.Models
{
    public class PersonList
    {
        public List<Person>? People { get; set; }

        // For eventual pagination
        public int? TotalCount { get; set; }
    }
}
