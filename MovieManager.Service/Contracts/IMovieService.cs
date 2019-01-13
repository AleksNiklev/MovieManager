using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieManager.Data.Models;
using X.PagedList;

namespace MovieManager.Service.Contracts
{
    public interface IMovieService
    {
        Task<IList<Movie>> GetMoviesAsync();
        Task<Movie> GetMovieAsync(int id);
        Task<Movie> AddMovie(string title, string director, DateTime releaseDate);
        Task<Movie> Update(int id, string title, string director, DateTime releaseDate);
    }
}