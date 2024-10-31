using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMDB2.Models
{
    [Table("movies", Schema = "tmdb_users")]
    public class Movie
    {
        // Variables created by chatgpt based on the JSON response. remember to ask if limiting variables to only those used is best practice or not.

        //Todo:  Genre,  ProductionCompany, Productioncountry, spoken language is also used by Series. Create seperate models???

        [Key]
        [Column("idmovies")]
        public int Id { get; set; }

        [Column("Title")]
        public string? Title { get; set; }

        // not stored on tmdb, canot be retrieved from api call. is only stored locally on tmdb_users
        [Column("favorite_amount")]
        public int FavoriteAmount { get; set; }


        //Many to many relationship with User
        public ICollection<FavoriteMovie> FavoriteMovies { get; set; }

        // As i only save 3 variablesin my local database, i have to delcare         [NotMapped] before all the other variables......  consider looking into partial classes


        [NotMapped]
        public bool Adult { get; set; }

        [NotMapped]
        [JsonProperty("backdrop_path")]
        public string? BackdropPath { get; set; }
        
        [NotMapped]
        public object? BelongsToCollection { get; set; }

        [NotMapped]
        public int Budget { get; set; }

        [NotMapped]
        public string? Homepage { get; set; }

        [NotMapped]
        [JsonProperty("imdb_id")]
        public string? ImdbId { get; set; }

        [NotMapped]
        [JsonProperty("origin_country")]
        public List<string>? OriginCountry { get; set; }

        [NotMapped]
        [JsonProperty("original_language")]
        public string? OriginalLanguage { get; set; }

        [NotMapped]
        [JsonProperty("original_title")]
        public string? OriginalTitle { get; set; }

        [NotMapped]
        public string? Overview { get; set; }

        [NotMapped]
        public double Popularity { get; set; }

        [NotMapped]
        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [NotMapped]
        [JsonProperty("release_date")]
        public string? ReleaseDate { get; set; }

        [NotMapped]
        public long Revenue { get; set; }

        [NotMapped]
        public int Runtime { get; set; }

        [NotMapped]
        public string? Status { get; set; }

        [NotMapped]
        public string? Tagline { get; set; }

        [NotMapped]
        public bool Video { get; set; }

        [NotMapped]
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [NotMapped]
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }


        // Lists that i dont want to map

        [NotMapped]
        public List<Genre>? Genres { get; set; }

        [NotMapped]
        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage>? SpokenLanguages { get; set; }

        [NotMapped]
        [JsonProperty("production_companies")]
        public List<ProductionCompany>? ProductionCompanies { get; set; }

        [NotMapped]
        [JsonProperty("production_countries")]
        public List<ProductionCountry>? ProductionCountries { get; set; }


    }
    [NotMapped]
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    [NotMapped]
    public class ProductionCompany
    {
        public int Id { get; set; }

        [JsonProperty("logo_path")]
        public string? LogoPath { get; set; }

        public string? Name { get; set; }

        [JsonProperty("origin_country")]
        public string? OriginCountry { get; set; }
    }
    [NotMapped]
    public class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string? Iso31661 { get; set; }

        public string? Name { get; set; }
    }
    [NotMapped]
    public class SpokenLanguage
    {
        [JsonProperty("english_name")]
        public string? EnglishName { get; set; }

        [JsonProperty("iso_639_1")]
        public string? Iso6391 { get; set; }

        public string? Name { get; set; }
    }




}
