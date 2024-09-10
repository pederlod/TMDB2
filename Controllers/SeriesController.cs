using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDB2.Models;
using TMDB2.Data;

namespace TMDB2.Controllers
{
    public class SeriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SeriesController> _logger;
        private readonly MyDbContext _context;
        public SeriesController(HttpClient httpClient, ILogger<SeriesController> logger, MyDbContext context)
        {
            _httpClient = httpClient;
            _logger = logger;
            _context = context;
        }
        // Series search result display view
        public async Task<IActionResult> Search(string query, int page = 1)
        {
            var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
            string url;

            // if nothing is searched for, bring out trending
            if (!string.IsNullOrEmpty(query))
            {
                url = $"https://api.themoviedb.org/3/search/tv?query={query}&include_adult=false&language=en-US&page={page}&api_key={apiKey}";
            }
            else
            {
                url = $"https://api.themoviedb.org/3/trending/tv/day?language=en-US={query}&include_adult=false&language=en-US&page={page}&api_key={apiKey}";
            }


            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var apiResult = JsonConvert.DeserializeObject<SeriesApiResponse>(response);

                var model = new SeriesList
                {
                    Series = apiResult.Results,
                    TotalCount = apiResult.TotalResults,
                    CurrentPage = apiResult.Page ?? 1,
                    TotalPages = apiResult.TotalPages ?? 1
                };
                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request to {url} failed with status code {ex.StatusCode}: {ex.Message}");
                return StatusCode((int)ex.StatusCode, "Error retrieving series search data.");
            }
        }

        // Single Series view. just copied from movie atm
        public async Task<IActionResult> Index(int seriesId)
        {
            var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
            var url = $"https://api.themoviedb.org/3/tv/{seriesId}?api_key={apiKey}&language=en-US";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var series = JsonConvert.DeserializeObject<Series>(response);

                // Check if the user is authenticated
                if (User.Identity.IsAuthenticated)
                {
                    var userId = GetUserId();
                    // Check if the series is already in the user's favorites
                    var isFavorite = _context.FavoriteSeries.Any(fs => fs.Iduser == userId && fs.Idseries == seriesId);
                    ViewBag.IsFavorite = isFavorite;



                }

                // Check if the series exists in the local database
                var dbSeries = _context.Series.FirstOrDefault(m => m.Id == seriesId);
                if (dbSeries != null)
                {
                    // Set the favorite amount from the local database
                    series.FavoriteAmount = dbSeries.FavoriteAmount;
                }
                else
                {
                    // If the series doesn't exist locally, set FavoriteAmount to 0
                    series.FavoriteAmount = 0;
                }

                return View(series);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request to {url} failed with status code {ex.StatusCode}: {ex.Message}");
                return StatusCode((int)ex.StatusCode, "Error retrieving Series data.");
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
    }
}
//"https://api.themoviedb.org/3/search/tv?query=big%20bang&include_adult=false&language=en-US&page=1"