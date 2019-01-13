using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Data.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(200)]
        public string Title { get; set; }

        [Required, MinLength(2), MaxLength(100)]
        public string Director { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
