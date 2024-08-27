using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace TMDB2.Models
{
    [Table("users", Schema = "tmdb_users")]
    public class User
    {
        // yea... the field names are poorly chosen and inconsistent.... What was i thinking...

        [Key]
        [Column("iduser")]
        public int Iduser { get; set; }

        [Column("user_name")]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Column("password")]
        //remove required after field update
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }


        [Column("salt")]
        [Required(ErrorMessage = "Wrong password")]
        public byte[] Salt { get; set; }


        // Many to many relationship for favorites
        // The = new List<FavoriteMovie>(); indicates that they are optional in the upon creation for some reason
        public ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();
        public ICollection<FavoriteSeries> FavoriteSeries { get; set; } = new List<FavoriteSeries>();
    }
}