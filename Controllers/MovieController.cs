﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDB2.Models;
using TMDB2.Data;

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
        // Movies search result display view
        public async Task<IActionResult> Search(string query, string genre, string actor, int? year, int page = 1)
		{
			var apiKey = "Original ApiKey, Do Not Steal";
			string url;

			//if query is empty, use filters.
			if (!string.IsNullOrEmpty(query))
			{
				url = $"https://api.themoviedb.org/3/search/movie?query={query}&include_adult=false&language=en-US&page={page}&api_key={apiKey}";
			}
			else
			{
				var genreId = await GetGenreId(genre);
				var actorId = await GetActorId(actor);

				url = $"https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page={page}&sort_by=popularity.desc&api_key={apiKey}";

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

                if(year == null && actorId == null && genreId == null)
                {
                    url = $"https://api.themoviedb.org/3/trending/movie/day?language=en-US&page={page}&api_key={apiKey}";
                }
            }
			try
			{
				var response = await _httpClient.GetStringAsync(url);

				// uncomment if logging Json response is needed
				//_logger.LogInformation("API Response: " + response);

				var apiResult = JsonConvert.DeserializeObject<MovieApiResponse>(response);

				_logger.LogInformation($"Deserialized TotalPages from response: {apiResult.TotalPages}");

				var model = new MovieList
				{
					Movies = apiResult.Results,
					TotalCount = apiResult.TotalResults,
					CurrentPage = apiResult.Page ?? 1,
					TotalPages = apiResult.TotalPages ?? 1
				};

				ViewBag.Query = query;
				ViewBag.Genre = genre;
				ViewBag.Actor = actor;
				ViewBag.Year = year;


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
                    ViewBag.IsFavorite = isFavorite;  // This needs to be correct


                    Console.WriteLine("Does the controller think this is a favorite? " + isFavorite);
					_logger.LogInformation($"Is this logger working?");

				}
                else
                {
                    ViewBag.IsFavorite = false;
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