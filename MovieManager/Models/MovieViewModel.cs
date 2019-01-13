using MovieManager.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
        }

        public MovieViewModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Title = movie.Title;
            this.Director = movie.Director;
            this.ReleaseDate = movie.ReleaseDate;
        }

        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(200)]
        public string Title { get; set; }

        [Required, MinLength(2), MaxLength(100)]
        public string Director { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
