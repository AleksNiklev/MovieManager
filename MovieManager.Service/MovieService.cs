using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.Models;
using MovieManager.Service.Contracts;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Service
{
    public class MovieService : IMovieService
    {
        private readonly MovieManagerContext context;

        public MovieService(MovieManagerContext context)
        {
            this.context = context;
        }

        public async Task<IList<Movie>> GetMoviesAsync()
        {
            return await this.context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            return await this.context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie> AddMovie(string title, string director, DateTime releaseDate)
        {
            var result = await this.context.Movies.AddAsync(
                new Movie()
                {
                    Title = title,
                    Director = director,
                    ReleaseDate = releaseDate
                });
            await this.context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Movie> Update(int id, string title, string director, DateTime releaseDate)
        {
            var movie = new Movie()
            {
                Id = id,
                Title = title,
                Director = director,
                ReleaseDate = releaseDate
            };

            var result = this.context.Movies.Update(movie);
            await this.context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
