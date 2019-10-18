using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieWebApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieWebApp.Models;
using System.Globalization;
using MovieWebApp.Interfaces;
using MovieWebApp.Services;

namespace MovieWebApplication.Controllers
{

    [Route("api/[controller]/[action]")]
    public class MoviesController : ControllerBase
    {
        private readonly IApiService _apiservice;
        public MoviesController(IApiService apiService)
        {
            _apiservice = apiService;
            _apiservice.GetAveragRating();
        }
        /// <summary>
        /// Index Method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ContentResult Index()
        {
            return Content("Welcome To MoviesApi");
        }

        /// <summary>
        /// Search Api
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ApiA([FromQuery]string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest("Invalid Search Criteria");

            var searchTxt = term.ToLower();
            var response = await _apiservice.SearchMovieByCriteria(searchTxt);
            if (response.Count > 0)
                return new OkObjectResult(response);
            else
                return NotFound("No Results Found.Please try different search criteria");
        }

        [HttpGet]
        public async Task<IActionResult> ApiB()
        {
            var response = await _apiservice.TopRatingMovies();
            if (response == null)
                return NotFound("Sorry.No Ratings Found.");

            return new OkObjectResult(response);
        }

         /// <summary>
        /// Add/Update User Ratings
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> ApiC(UserMovieRatingcs queryParams)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            if (queryParams.UserId <= 0 || queryParams.MovieId <= 0)
                return BadRequest("Input Criteria Failed.");

            var userInfo = await _apiservice.GetUserDetails(queryParams.UserId);
            string responseMsg = string.Empty;
            if (userInfo != null && userInfo.Id > 0)
            {
                var movieDetails = await _apiservice.GetMovieDetails(queryParams.MovieId);
                if (movieDetails != null && movieDetails.Id > 0)
                {
                    var trackingInfo = _apiservice.GetUserMovieRatings(queryParams.MovieId, queryParams.UserId);
                    if (trackingInfo != null && trackingInfo.Id > 0)
                    {
                        //Updating Existing Record
                        trackingInfo.Rating = queryParams.Rating;
                        _apiservice.UpdateMovieRating(trackingInfo);
                        responseMsg = "MovieRating Updated Successfully";
                    }
                    else
                    {
                        //Adding Record
                        _apiservice.AddMovieRating(queryParams);
                         responseMsg = "Movie Rating Added Successfully";
                    }
                    return Ok(responseMsg);
                }
                return NotFound("No Movies Found.Input MovieId Is Not Valid MoviedId");
            }
            return NotFound("Input UserId Is Not Valid UserId");
        }

        /// <summary>
        /// Get Top 5 Highest Rating Movies
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ApiD(int userid)
        {
            if (userid <= 0)
                return BadRequest("Please provide valid UserId.");

            var response = await _apiservice.GetMoviesWithHighestRating(userid);
            if (response == null)
                return NotFound("Sorry.No Ratings Found.");

            return new OkObjectResult(response);
        }
    }
}