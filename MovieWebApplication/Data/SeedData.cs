using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieWebApp.Data;
using MovieWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApp.Data
{
    public class SeedData
    {
        public static void AddTestData(ApiContext context)
        {
           var movie1 = new Movie
            {
                Id = 1,
                Title = "Titanic",
                Genre = "Thriller",
                YearOfRelease = 2008,
                RunningTime = 120
            };
        
            var user1 = new User
            {
                Id = 1,
                UserName = "Test1"
            };

            
            var movieRating1 = new UserMovieRatingcs
            { 
                Id=1,
                UserId = user1.Id,
                MovieId = movie1.Id,
                Rating =  3
            };
            context.UserMovieRatings.Add(movieRating1);
            context.User.Add(user1);
            context.Movie.Add(movie1);

            var movie2 = new Movie
            {
                Id = 2,
                Title = "Transformer",
                Genre = "Thriller,Fiction",
                YearOfRelease = 2000,
                RunningTime = 180
            };

            context.Movie.Add(movie2);
            var movie3 = new Movie
            {
                Id = 3,
                Title = "Jurassic Park",
                Genre = "Thriller,Horror",
                YearOfRelease = 2002,
                RunningTime = 120
            };

            context.Movie.Add(movie3);
            var movie4 = new Movie
            {
                Id = 4,
                Title = "Scream",
                Genre = "Horror",
                YearOfRelease = 2006,
                RunningTime = 110
            };

            context.Movie.Add(movie4);

            var movie5 = new Movie
            {
                Id = 5,
                Title = "Avatar",
                Genre = "ScienceFiction",
                YearOfRelease = 1988,
                RunningTime = 160
            };

            context.Movie.Add(movie5);

  

            var user2 = new User
            {
                Id = 2,
                UserName = "Test2"
            };
            context.User.Add(user2);

            var movieRating2 = new UserMovieRatingcs
            {
                Id=2,
                UserId = user1.Id,
                MovieId = movie2.Id,
                Rating = 5
            };
            context.UserMovieRatings.Add(movieRating2);

            var user3 = new User
            {
                Id = 3,
                UserName = "Test3"
            };
            context.User.Add(user3);

            var movieRating3 = new UserMovieRatingcs
            {
                Id=3,
                UserId = user1.Id,
                MovieId = movie3.Id,
                Rating = 4
            };
            context.UserMovieRatings.Add(movieRating3);

            var user4 = new User
            {
                Id = 4,
                UserName = "Test4"
            };
            context.User.Add(user4);

            var movieRating4 = new UserMovieRatingcs
            {
                Id=4,
                UserId = user1.Id,
                MovieId = movie5.Id,
                Rating = 3
            };
            context.UserMovieRatings.Add(movieRating4);
            var user5 = new User
            {
                Id = 5,
                UserName = "Test5"
            };
            context.User.Add(user5);

            var movieRating5 = new UserMovieRatingcs
            {
                Id=5,
                UserId = user1.Id,
                MovieId = movie4.Id,
                Rating = 3
            };
            context.UserMovieRatings.Add(movieRating5);
            context.SaveChanges();

        }
    }
}
