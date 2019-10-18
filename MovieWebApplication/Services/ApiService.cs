using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieWebApp.Data;
using MovieWebApp.Interfaces;
using MovieWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebApp.Services
{
    public class ApiService : IApiService
    {
        private readonly ApiContext _context;
        private readonly ILogger<ApiService> _logger;

        public ApiService(ApiContext context, ILogger<ApiService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async void GetAveragRating()
        {
            try
            {
                var results = from row1 in _context.Movie
                              join row in _context.UserMovieRatings
                              on row1.Id equals row.MovieId
                              group row by row.MovieId into rows
                              select new
                              {
                                  id = rows.Key,
                                  averageScore = Math.Round(rows.Average(row => row.Rating) * 2, 0, MidpointRounding.AwayFromZero) / 2
                              };
                await _context.Movie.ForEachAsync(d => d.AverageRating = results.Where(sd => sd.id == d.Id).Select(x => Convert.ToDecimal(x.averageScore)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In GetAverageRating Method");
            }
        }
        public async Task<List<Movie>> SearchMovieByCriteria(string searchTxt)
        {
            try
            {
                var response = await _context.Movie
                     .Where(b => b.Genre.ToLower().Contains(searchTxt)
                     || b.Title.ToLower().Contains(searchTxt)).Select(x => x)
                      .ToListAsync();

                if (response != null && response.Count.Equals(0))
                {
                    int n;
                    bool isNumeric = int.TryParse(searchTxt, out n);
                    if (isNumeric)
                    {
                        response = await _context.Movie
                        .Where(b => b.YearOfRelease.Equals(n)).Select(x => x).ToListAsync();
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In SearchMovieByCriteria Method ");
                return null;
            }
        }
        public async Task<List<Movie>> TopRatingMovies()
        {
            try
            {
                return await _context.Movie.OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title).Select(x => x).Take(5).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In TopRatingMovies Method ");
                return null;
            }
        }
        public async Task<List<Movie>> GetMoviesWithHighestRating(int userid)
        {
            try
            {
                var response = await _context.UserMovieRatings.Where(x => x.UserId == userid).OrderByDescending(x => x.Rating).Select(x => x.MovieId).Take(5).ToListAsync();
                if (response != null && response.Count > 0)
                {
                    var movieFilteredList = await _context.Movie.Where(x => response.Contains(x.Id)).OrderBy(x => response.IndexOf(x.Id)).ThenBy(x => x.Title).Select(x => x).ToListAsync();
                    return movieFilteredList;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In GetMoviesWithHighestRating Method ");
                return null;
            }
        }
        public async void AddMovieRating(UserMovieRatingcs queryParams)
        {
            try
            {
                var newID = _context.UserMovieRatings.Select(x => x.Id).Max() + 1;
                var trackingInfo = new UserMovieRatingcs()
                {
                    Id = newID,
                    UserId = queryParams.UserId,
                    MovieId = queryParams.MovieId,
                    Rating = queryParams.Rating
                };
                await _context.UserMovieRatings.AddAsync(trackingInfo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In AddMovieRating Method ");
            }
         
        }

        public void UpdateMovieRating(UserMovieRatingcs trackingInfo)
        {
            try
            {
                //Updating Existing Record
                _context.UserMovieRatings.Update(trackingInfo);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In UpdateMovieRating Method ");
            }
           
        }

        public async Task<User> GetUserDetails(int userid)
        {
            try
            {
                return await _context.User.FindAsync(userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In GetUserDetails Method ");
                return null;
            }
        }
        public async Task<Movie> GetMovieDetails(int movieid)
        {
            try
            {
                return await _context.Movie.FindAsync(movieid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In GetMovieDetails Method ");
                return null;
            }
        }

        public UserMovieRatingcs GetUserMovieRatings(int movieid, int userid)
        {
            try
            {
                return _context.UserMovieRatings.Where(x => x.MovieId.Equals(movieid) && x.UserId.Equals(userid)).Select(x => x).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception In GetUserMovieRatings Method ");
                return null;
            }
          
        }
    }
}
