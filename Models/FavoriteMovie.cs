using System.ComponentModel.DataAnnotations.Schema;

namespace TMDB2.Models
{
    [Table("favorite_movies", Schema = "tmdb_users")]
    public class FavoriteMovie
    {
        [Column("iduser")]
        public int Iduser { get; set; }
        [Column("idmovies")]
        public int Idmovies { get; set; }

        // I assumed this should be unmapped, as they are not really variables in the database, but this error keeps haunting me:
        // This is because the property is also part of a foreign key for which the principal entity in the relationship is not known

        public User User { get; set; }

        public Movie Movie { get; set; }
    }
}