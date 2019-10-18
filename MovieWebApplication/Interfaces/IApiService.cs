using Microsoft.EntityFrameworkCore;
using MovieWebApp.Data;
using MovieWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApp.Interfaces
{
    public interface IApiService
    {
        void GetAveragRating();
        Task<List<Movie>> SearchMovieByCriteria(string term);
        Task<List<Movie>> TopRatingMovies();
        Task<List<Movie>> GetMoviesWithHighestRating( int userid);
        void AddMovieRating(UserMovieRatingcs queryParams);
        void UpdateMovieRating(UserMovieRatingcs trackingInfo);
        Task<User> GetUserDetails(int userid);
        Task<Movie> GetMovieDetails(int movieid);
        UserMovieRatingcs GetUserMovieRatings(int movieid, int userid);
    }


}
