using System.ComponentModel.DataAnnotations.Schema;

namespace TMDB2.Models
{
    [Table("favorite_series", Schema = "tmdb_users")]
    public class FavoriteSeries
    {
        [Column("iduser")]
        public int Iduser { get; set; }
        [Column("idseries")]
        public int Idseries { get; set; }

        // I assumed this should be unmapped, as they are not really variables in the database, but this error keeps haunting me:
        // This is because the property is also part of a foreign key for which the principal entity in the relationship is not known

        //[NotMapped]
        public User User { get; set; }
        //[NotMapped]
        public Series Serie { get; set; }
        
    }
}