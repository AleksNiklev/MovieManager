using Microsoft.EntityFrameworkCore;
using MovieManager.Data.Models;

namespace MovieManager.Data
{
    public class MovieManagerContext : DbContext
    {
        public MovieManagerContext()
        {
        }

        public MovieManagerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=MovieManager;Trusted_Connection=True;");
            }
        }
    }
}
