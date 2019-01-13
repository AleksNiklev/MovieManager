using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManager.Data;
using MovieManager.Data.Models;
using MovieManager.Service;
using MovieManager.Service.Contracts;

namespace MovieManager.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly MovieManagerContext context;

        public MoviesApiController(MovieManagerContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Movie>>> GetMovies()
        {
            return await this.context.Movies.ToListAsync();
        }

        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<IList<Movie>>> GetMoviesPage(int page, int size)
        {
            int skip = (page - 1) * size;
            var movies = this.context.Movies.Skip(skip).Take(size);
            return await movies.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.context.Movies.Add(movie);
            await this.context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await this.context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid || id != movie.Id)
            {
                return BadRequest(ModelState);
            }

            this.context.Entry(movie).State = EntityState.Modified;
            await this.context.SaveChangesAsync();

            return NoContent();
        }

    }
}