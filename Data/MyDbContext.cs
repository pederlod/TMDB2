using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TMDB2.Models;
// this class is for managing the comunication with my local database.
namespace TMDB2.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; }
        public DbSet<FavoriteSeries> FavoriteSeries { get; set; }

        // This is for logging my SQL series. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable logging of SQL queries
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
                .EnableSensitiveDataLogging(); // Optional: Shows parameters in SQL queries
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Console.WriteLine("OnModelCreating is being called.");

            // Specify the correct table name for the User entity
            modelBuilder.Entity<User>().ToTable("users", schema: "tmdb_users");

            modelBuilder.Entity<FavoriteMovie>()
                .HasKey(fm => new { fm.Iduser, fm.Idmovies });

            modelBuilder.Entity<FavoriteMovie>()
                .HasOne(fm => fm.User)
                .WithMany(u => u.FavoriteMovies)
                .HasForeignKey(fm => fm.Iduser)
                // just to see if this.IsRequired() gets error
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteMovie>()
                .HasOne(fm => fm.Movie)
                .WithMany(m => m.FavoriteMovies)
                .HasForeignKey(fm => fm.Idmovies)
                // just to see if this.IsRequired() gets error
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);




            // TODO update composite key on series to update

            modelBuilder.Entity<FavoriteSeries>()
                .HasKey(fs => new { fs.Iduser, fs.Idseries });

            modelBuilder.Entity<FavoriteSeries>()
                .HasOne(fs => fs.User)
                .WithMany(u => u.FavoriteSeries)
                .HasForeignKey(fs => fs.Iduser)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteSeries>()
                .HasOne(fs => fs.Serie)
                .WithMany(s => s.FavoriteSeries)
                .HasForeignKey(fs => fs.Idseries)
                .OnDelete(DeleteBehavior.Cascade);


            // Keyless objects. ( I dont store these in my database)

            modelBuilder.Entity<Genre>()
                .HasNoKey();

            modelBuilder.Entity<ProductionCompany>()
                .HasNoKey();

            modelBuilder.Entity<ProductionCountry>()
                .HasNoKey();

            modelBuilder.Entity<SpokenLanguage>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}