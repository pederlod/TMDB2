using Microsoft.AspNetCore.Mvc;
using TMDB2.Data;
using TMDB2.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using TMDB2.ViewComponents.Components;

public class FavoriteController : Controller
{
    private readonly MyDbContext _context;

    public FavoriteController(MyDbContext context)
    {
        _context = context;
    }

    // Display the user's favorite list

    // Variables are different here. not usre if idmovies and Id can be used in tandem
    // The intent is to use the id from the local database, to make a API call to TMDB to get the rest of the information 


  
    public IActionResult Index()
    {
        var userId = GetUserId();

        var favoriteList = new FavoriteList
        {
            Iduser = userId,
            Movies = _context.Movies
                .Join(_context.FavoriteMovies, m => m.Id, fm => fm.Idmovies,
                      (m, fm) => new { m, fm })
                .Where(x => x.fm.Iduser == userId)
                .Select(x => x.m)
                .ToList(),

            Series = _context.Series
                .Join(_context.FavoriteSeries, s => s.Id, fs => fs.Idseries,
                      (s, fs) => new { s, fs })
                .Where(x => x.fs.Iduser == userId)
                .Select(x => x.s)
                .ToList()
        };

        return View(favoriteList);
    }


    // Add a series to the favorites list


    public IActionResult AddFavoriteMovie(int idmovies, string title)
    {
        var userId = GetUserId();

        // Check if the movie is already a favorite for this user
        var existingFavorite = _context.FavoriteMovies
            .FirstOrDefault(fm => fm.Iduser == userId && fm.Idmovies == idmovies);

        if (existingFavorite != null)
        {
            // Movie is already a favorite, handle this case as needed
            // You can return a message or just skip the insertion
            Console.WriteLine("Movie is already a favorite.");



            //_context.FavoriteMovies.Remove(existingFavorite);
            //_context.SaveChanges();
            return Json(new { success = false, message = "Movie is already in your favorites." });
        }


        // Ensure the movie exists in the database
        var movie = _context.Movies.FirstOrDefault(m => m.Id == idmovies);
        if (movie == null)
        {
            movie = new Movie { Id = idmovies, Title = title, FavoriteAmount = 0 };
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        var favoriteMovie = new FavoriteMovie
        {
            Iduser = userId,
            Idmovies = idmovies
        };

        _context.FavoriteMovies.Add(favoriteMovie);
        _context.SaveChanges();

       

        return Json(new { success = true });
    }

    public IActionResult AddFavoriteSeries(int idseries, string title)
    {
        var userId = GetUserId();

        var series = _context.Series.FirstOrDefault(s => s.Id == idseries);
        if (series == null)
        {
            series = new Series { Id = idseries, Name = title, FavoriteAmount = 0 };
            _context.Series.Add(series);
            _context.SaveChanges();
        }

        var favoriteSeries = _context.FavoriteSeries
            .FirstOrDefault(fs => fs.Iduser == userId && fs.Idseries == idseries);

        if (favoriteSeries != null)
        {
            return Json(new { success = false, message = "Series is already in your favorites." });
        }

        _context.FavoriteSeries.Add(new FavoriteSeries { Iduser = userId, Idseries = idseries });
        _context.SaveChanges();

        

        return Json(new { success = true });
    }


    public IActionResult RemoveFavoriteMovie(int idmovies)
    {
        var userId = GetUserId();
        Console.WriteLine($"Trying to remove favorite movie. UserId: {userId}, MovieId: {idmovies}");


        // Ensure the idmovies is treated as an integer

        var favoriteMovie = _context.FavoriteMovies
            .FirstOrDefault(fm => fm.Iduser == userId && fm.Idmovies == idmovies);

        if (favoriteMovie != null)
        {
            _context.FavoriteMovies.Remove(favoriteMovie);
            _context.SaveChanges();


            return Json(new { success = true });
        
            
        }
        Console.WriteLine("Favorite removal failed......");
        Console.WriteLine($"Favorite movie not found. UserId: {userId}, MovieId: {idmovies}");
        Console.WriteLine("Is Favorite movie null? " + favoriteMovie != null);
        Console.WriteLine("userId: " +  userId);


        return Json(new { success = false });
    }

    public IActionResult RemoveFavoriteSeries(int idseries)
    {
        var userId = GetUserId();

        var favoriteSeries = _context.FavoriteSeries
            .FirstOrDefault(fs => fs.Iduser == userId && fs.Idseries == idseries);

        if (favoriteSeries != null)
        {
            _context.FavoriteSeries.Remove(favoriteSeries);
            _context.SaveChanges();

           

            return Json(new { success = true });
        }

        return Json(new { success = false });
    }




    private int GetUserId()
    {
        // Find the claim that contains the user's ID
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

        // If the claim is not found, handle it appropriately (e.g., throw an exception or return a default value)
        if (userIdClaim == null)
        {
            throw new Exception("User ID claim not found.");
        }

        // Parse the claim value to an integer and return it
        return int.Parse(userIdClaim.Value);
    }

    //updates sidebar when a movie or series is favorited or unfavorited
    public IActionResult UpdateFavoriteSidebar()
    {
        Console.WriteLine("UpdateFavoriteSidebar is called!");

        return ViewComponent("FavoriteSidebar");
    }


}
