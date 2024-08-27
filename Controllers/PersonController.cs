using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMDB2.Models;
using Microsoft.Extensions.Logging;

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
    public async Task<IActionResult> Search(string query)
    {
        var apiKey = "bf1f911dcc8d683db6962773bd88ca51";
        var url = $"https://api.themoviedb.org/3/search/person?query={query}&include_adult=false&language=en-US&page=1&api_key={apiKey}";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var apiResult = JsonConvert.DeserializeObject<PersonApiResponse>(response);

            var model = new PersonList { People = apiResult.Results, TotalCount = apiResult.TotalResults };
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

//https://api.themoviedb.org/3/search/person?query=Chris&include_adult=false&language=en-US&page=1"