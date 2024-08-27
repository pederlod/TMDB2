namespace TMDB2.Models
{
    public class ActorApiResponse
    {
        public List<Actor> Results { get; set; }
    }

    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}