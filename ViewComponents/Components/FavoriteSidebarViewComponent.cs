namespace TMDB2.ViewComponents.Components;


using Microsoft.AspNetCore.Mvc;
using TMDB2.Data;
using TMDB2.Models;
using System.Linq;
using System.Security.Claims;

public class FavoriteSidebarViewComponent : ViewComponent
{
    private readonly MyDbContext _context;

    public FavoriteSidebarViewComponent(MyDbContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Content(string.Empty);
        }

        var claimsPrincipal = User as ClaimsPrincipal;
        var userId = int.Parse(claimsPrincipal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);

        var favoriteList = new FavoriteList
        {
            Movies = _context.FavoriteMovies
                .Where(fm => fm.Iduser == userId)
                .Select(fm => fm.Movie)
                .ToList(),
            Series = _context.FavoriteSeries
                .Where(fs => fs.Iduser == userId)
                .Select(fs => fs.Serie)
                .ToList()
        };

        return View(favoriteList);
    }
}
