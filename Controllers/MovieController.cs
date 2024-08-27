using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TMDB2.Data;
using TMDB2.Models;

namespace TMDB2.Controllers
{
    public class MovieController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MovieController> _logger;
        private readonly MyDbContext _context;

        public MovieController(HttpClient httpClient, ILogger<MovieController> logger, MyDbContext context)
        {
            _httpClient = httpClient;
            _logger = logger;
            _context = context;
        }
        /*
        // Old Simple Movie search result display view. TODO remove when new Search method is implemented
        public async Task<IActionResult> Search(string query)
        {
            var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
            var url = $"https://api.themoviedb.org/3/search/movie?query={query}&include_adult=false&language=en-US&page=1&api_key={apiKey}";

            var response = await _httpClient.GetStringAsync(url);
            var apiResult = JsonConvert.DeserializeObject<MovieApiResponse>(response);

            var model = new MovieList { Movies = apiResult.Results, TotalCount = apiResult.TotalResults };
            return View(model);
        }
        */

        // More advanced movie search results based on genre and/or actor and/or primaryReleaseYear. requires genre and actor object

        //TODO. Implement autofill or dropdown? current filter requires very precise precise input.

    public async Task<IActionResult> Search(string query, string genre, string actor, int? year)
    {
        var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
        string url;

        if (!string.IsNullOrEmpty(query))
        {
            url = $"https://api.themoviedb.org/3/search/movie?query={query}&include_adult=false&language=en-US&page=1&api_key={apiKey}";
        }
        else
        {
            var genreId = await GetGenreId(genre);
            var actorId = await GetActorId(actor);

            url = $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc&api_key={apiKey}";

            if (genreId != null)
            {
                url += $"&with_genres={genreId}";
            }

            if (actorId != null)
            {
                url += $"&with_cast={actorId}";
            }

            if (year != null)
            {
                url += $"&primary_release_year={year}";
            }
        }
        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var apiResult = JsonConvert.DeserializeObject<MovieApiResponse>(response);

            var model = new MovieList { Movies = apiResult.Results, TotalCount = apiResult.TotalResults };
            return View(model);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Request to {url} failed with status code {ex.StatusCode}: {ex.Message}");
            return StatusCode((int)ex.StatusCode, "Error retrieving Movie search data.");
        }


    }

    // http request to aquire the ID of genre and actor searched for. Should be improved to not be as strict
    private async Task<int?> GetGenreId(string genre)
    {
        if (string.IsNullOrEmpty(genre)) return null;
        var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
        var url = $"https://api.themoviedb.org/3/genre/movie/list?api_key={apiKey}&language=en-US";
        var response = await _httpClient.GetStringAsync(url);
        var genres = JsonConvert.DeserializeObject<GenreResponse>(response);

        var genreMatch = genres.Genres.FirstOrDefault(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));
        return genreMatch?.Id;
    }

    private async Task<int?> GetActorId(string actor)
    {
        if (string.IsNullOrEmpty(actor)) return null;
        var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
        var url = $"https://api.themoviedb.org/3/search/person?query={actor}&include_adult=false&language=en-US&page=1&api_key={apiKey}";
        var response = await _httpClient.GetStringAsync(url);
        var actorResult = JsonConvert.DeserializeObject<ActorApiResponse>(response);

        var actorMatch = actorResult.Results.FirstOrDefault();
        return actorMatch?.Id;
    }



    // Single movie view   
    public async Task<IActionResult> Index(int movieId)
    {

        var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
        var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={apiKey}&language=en-US";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var movie = JsonConvert.DeserializeObject<Movie>(response);

            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var userId = GetUserId();
                // Check if the movie is already in the user's favorites
                var isFavorite = _context.FavoriteMovies.Any(fm => fm.Iduser == userId && fm.Idmovies == movieId);
                ViewBag.IsFavorite = isFavorite;



                }


                // Check if the movie exists in the local database
                var dbMovie = _context.Movies.FirstOrDefault(m => m.Id == movieId);
            if (dbMovie != null)
            {
                // Set the favorite amount from the local database
                movie.FavoriteAmount = dbMovie.FavoriteAmount;
            }
            else
            {
                // If the movie doesn't exist locally, set FavoriteAmount to 0
                movie.FavoriteAmount = 0;
            }


            return View(movie);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Request to {url} failed with status code {ex.StatusCode}: {ex.Message}");
            return StatusCode((int)ex.StatusCode, "Error retrieving Movie data.");
        }
    }


    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new Exception("User ID claim not found.");
        }
        return int.Parse(userIdClaim.Value);
    }

    //favorite movie database communication



    }
}