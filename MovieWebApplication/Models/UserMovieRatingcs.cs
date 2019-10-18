using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MovieWebApp.Models
{
    public class UserMovieRatingcs
    {
        public int Id { get; set; }
        [FromQuery(Name = "userId")]
        [BindRequired]
        public int UserId {get;set;}

        [FromQuery(Name = "movieId")]
        [BindRequired]
        public int MovieId { get; set; }

        [FromQuery(Name = "rating")]
        [BindRequired]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
  
}
