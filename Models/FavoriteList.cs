using System.ComponentModel.DataAnnotations.Schema;

namespace TMDB2.Models
{
    public class FavoriteList
    {

        public int Iduser { get; set; }
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Series> Series { get; set; } = new List<Series>();
    }
}