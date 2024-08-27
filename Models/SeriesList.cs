namespace TMDB2.Models
{
    public class SeriesList
    {
        public List<Series>? Series { get; set; }

        // For eventual pagination
        public int? TotalCount { get; set; }
    }
}

