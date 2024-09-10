using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMDB2.Models;


namespace TMDB2.Controllers
{
    public class PersonController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PersonController> _logger;

        public PersonController(HttpClient httpClient, ILogger<PersonController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

        }
        // Person search result display view
        public async Task<IActionResult> Search(string query, int page = 1)
        {
            var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
            string url;

            // if nothing is searched for, bring out trending
            if (!string.IsNullOrEmpty(query))
            {
                url = $"https://api.themoviedb.org/3/search/person?query={query}&include_adult=false&language=en-US&page={page}&api_key={apiKey}";
            }
            else
            {
                url = $"https://api.themoviedb.org/3/trending/person/day?language=en-US{query}&include_adult=false&language=en-US&page={page}&api_key={apiKey}";
            }
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var apiResult = JsonConvert.DeserializeObject<PersonApiResponse>(response);

                var model = new PersonList
                {
                    People = apiResult.Results,
                    TotalCount = apiResult.TotalResults,
                    CurrentPage = apiResult.Page ?? 1,
                    TotalPages = apiResult.TotalPages ?? 1
                };
                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request to {url} failed with status code {ex.StatusCode}: {ex.Message}");
                return StatusCode((int)ex.StatusCode, "Error retrieving person search data.");
            }
        }

        // Single Person view. added a catch
        public async Task<IActionResult> Index(int personId)
        {
            var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
            var url = $"https://api.themoviedb.org/3/person/{personId}?api_key={apiKey}&language=en-US";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var person = JsonConvert.DeserializeObject<Person>(response);
                return View(person);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request to {url} failed with status code {ex.StatusCode}: {ex.Message}");
                return StatusCode((int)ex.StatusCode, "Error retrieving person data.");
            }
        }
    }
}

//https://api.themoviedb.org/3/search/person?query=Chris&include_adult=false&language=en-US&page=1"