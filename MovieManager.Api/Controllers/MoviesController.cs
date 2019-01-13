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

namespace MovieManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieManagerContext context;

        public MoviesController(MovieManagerContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Movie>>> GetMovies()
        {
            return await this.context.Movies.ToListAsync();
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