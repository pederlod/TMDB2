using Newtonsoft.Json;

namespace TMDB2.Models
{
    public class PersonApiResponse
    {
        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("results")]
        public List<Person>? Results { get; set; }

        [JsonProperty("total_results")]
        public int? TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int? TotalPages { get; set; }
    }
}
