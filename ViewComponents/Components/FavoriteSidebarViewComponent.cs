using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TMDB2.Data;
using TMDB2.Models;

public class FavoriteSidebarViewComponent : ViewComponent
{
    private readonly MyDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly string apiKey = "bf1f911dcc8d683db6962773bd88ca51";

    public FavoriteSidebarViewComponent(MyDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }
    // Creates a list of the Users favorites, and fetches their details from TMDB API
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Content(string.Empty);
        }

        var claimsPrincipal = User as ClaimsPrincipal;
        var userId = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value);

        var favoriteList = new FavoriteList
        {
            Movies = await GetMoviesWithPosterPath(userId),
            Series = await GetSeriesWithPosterPath(userId)
        };

        return View(favoriteList);
    }

    private async Task<List<Movie>> GetMoviesWithPosterPath(int userId)
    {
        var movies = _context.FavoriteMovies
            .Where(fm => fm.Iduser == userId)
            .Select(fm => fm.Movie)
            .ToList();

        foreach (var movie in movies)
        {
            // Fetch the movie details including PosterPath from TMDB API
            var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{movie.Id}?api_key={apiKey}&language=en-US");
            var movieDetails = JsonConvert.DeserializeObject<Movie>(response);

            movie.PosterPath = movieDetails.PosterPath;
        }

        return movies;
    }

    private async Task<List<Series>> GetSeriesWithPosterPath(int userId)
    {
        var seriesList = _context.FavoriteSeries
            .Where(fs => fs.Iduser == userId)
            .Select(fs => fs.Serie)
            .ToList();

        foreach (var series in seriesList)
        {
            // Fetch the series details including PosterPath from TMDB API
            var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/tv/{series.Id}?api_key={apiKey}&language=en-US");
            var seriesDetails = JsonConvert.DeserializeObject<Series>(response);

            series.PosterPath = seriesDetails.PosterPath;
        }

        return seriesList;
    }
}

