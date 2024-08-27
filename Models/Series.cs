using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMDB2.Models
{

    [Table("series", Schema = "tmdb_users")]
    public class Series
    {
        // Variables created by chatgpt based on the JSON response. remember to ask if limiting variables to only those used is best practice or not.

        // todo, create models for Creator, Genre, Episode, Network, ProductionCompany, Productioncountry, season, spoken language
        // Genre,  ProductionCompany, Productioncountry, spoken language is also used by Movie. they are just out for now to remember variables for future models


        // I hope [Key] and JsonPropery can be used simoultaniously
        [Key]
        [Column("idseries")]
        [JsonProperty("id")]
        public int Id { get; set; }

        [Column("title")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Column("favorite_amount")]
        public int FavoriteAmount { get; set; }

        //Many to many relationship with User
        public ICollection<FavoriteSeries> FavoriteSeries { get; set; }


        // As i only save 3 variablesin my local database, i have to delcare         [NotMapped] before all the other variables......  consider looking into partial classes

        [NotMapped]
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [NotMapped]
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [NotMapped]
        [JsonProperty("first_air_date")]
        public string FirstAirDate { get; set; }

        [NotMapped]
        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [NotMapped]
        [JsonProperty("in_production")]
        public bool InProduction { get; set; }

        [NotMapped]
        [JsonProperty("languages")]
        public List<string> Languages { get; set; }

        [NotMapped]
        [JsonProperty("last_air_date")]
        public string LastAirDate { get; set; }

        [NotMapped]
        [JsonProperty("last_episode_to_air")]
        public Episode LastEpisodeToAir { get; set; }

        [NotMapped]
        [JsonProperty("next_episode_to_air")]
        public Episode NextEpisodeToAir { get; set; }

        [NotMapped]
        [JsonProperty("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }

        [NotMapped]
        [JsonProperty("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        [NotMapped]
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [NotMapped]
        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [NotMapped]
        [JsonProperty("overview")]
        public string Overview { get; set; }

        [NotMapped]
        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [NotMapped]
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [NotMapped]
        [JsonProperty("status")]
        public string Status { get; set; }

        [NotMapped]
        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [NotMapped]
        [JsonProperty("type")]
        public string Type { get; set; }

        [NotMapped]
        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [NotMapped]
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }


        //Lists i dont want mapped

        [NotMapped]
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [NotMapped]
        [JsonProperty("created_by")]
        public List<Creator> CreatedBy { get; set; }

        [NotMapped]
        [JsonProperty("networks")]
        public List<Network> Networks { get; set; }

        [NotMapped]
        [JsonProperty("episode_run_time")]
        public List<int> EpisodeRunTime { get; set; }

        [NotMapped]
        [JsonProperty("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [NotMapped]
        [JsonProperty("production_countries")]
        public List<ProductionCountry> ProductionCountries { get; set; }

        [NotMapped]
        [JsonProperty("seasons")]
        public List<Season> Seasons { get; set; }

        [NotMapped]
        [JsonProperty("spoken_languages")]
        public List<SpokenLanguage> SpokenLanguages { get; set; }

        [NotMapped]
        [JsonProperty("origin_country")]
        public List<string> OriginCountry { get; set; }
    }

    [NotMapped]
    public class Creator
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }

    /*  Allready in Movie.cs   temporarily kept, just in case i cant use that class here
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    */

    [NotMapped]
    public class Episode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("air_date")]
        public string AirDate { get; set; }

        [JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }

        [JsonProperty("episode_type")]
        public string EpisodeType { get; set; }

        [JsonProperty("production_code")]
        public string ProductionCode { get; set; }

        [JsonProperty("runtime")]
        public int Runtime { get; set; }

        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }

        [JsonProperty("show_id")]
        public int ShowId { get; set; }

        [JsonProperty("still_path")]
        public string StillPath { get; set; }
    }
    [NotMapped]
    public class Network
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    /*  Allready in Movie.cs   temporarily kept here for syntax
    public class ProductionCompany
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string Iso31661 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    */

    public class Season
    {
        [JsonProperty("air_date")]
        public string AirDate { get; set; }

        [JsonProperty("episode_count")]
        public int EpisodeCount { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }
    }

    /*  Allready in Movie.cs   temporarily kept here for syntax
    public class SpokenLanguage
    {
        [JsonProperty("english_name")]
        public string EnglishName { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    */
}
