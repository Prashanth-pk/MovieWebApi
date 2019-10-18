using MovieWebApp.Interfaces;
using MovieWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebAppTest.Services
{
    public class ApiServiceFake : IApiService
    {
        private readonly List<Movie> _movie;
        private readonly List<UserMovieRatingcs> _userratings;
        public ApiServiceFake()
        {
            _movie = new List<Movie>()
            {
                new Movie() {Id=1, AverageRating=Decimal.Parse("3.0"), Genre="Fiction,Horror",Title="Test1",YearOfRelease=2000,RunningTime=120},
                new Movie() {Id=2, AverageRating=Decimal.Parse("5.0"), Genre="Comedy",Title="Test2",YearOfRelease=2005,RunningTime=150}
            };
            _userratings = new List<UserMovieRatingcs>()
            {
                new UserMovieRatingcs() {Id=1,MovieId=1,Rating=4,UserId=1 },
                new UserMovieRatingcs() {Id=2,MovieId=2,Rating=3,UserId=1}
            };
        }

        public void GetAveragRating()
        {
            throw new NotImplementedException();
        }
        public Task<List<Movie>> SearchMovieByCriteria(string term)
        {
            throw new NotImplementedException();
        }
        public Task<List<Movie>> TopRatingMovies()
        {
            throw new NotImplementedException();
        }
        public Task<List<Movie>> GetMoviesWithHighestRating(int userid)
        {
            throw new NotImplementedException();
        }
        public void AddMovieRating(UserMovieRatingcs queryParams)
        {
            throw new NotImplementedException();
        }
        public void UpdateMovieRating(UserMovieRatingcs trackingInfo)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserDetails(int userid)
        {
            throw new NotImplementedException();
        }

       public Task<Movie> GetMovieDetails(int movieid)
        {
            throw new NotImplementedException();
        }
       public  UserMovieRatingcs GetUserMovieRatings(int movieid, int userid)
        {
            throw new NotImplementedException();
        }


    }
}
