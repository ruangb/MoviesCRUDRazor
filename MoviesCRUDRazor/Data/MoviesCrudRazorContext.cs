using Microsoft.EntityFrameworkCore;
using MoviesCRUDRazor.Models;

namespace MoviesCRUDRazor.Data
{
    public class MoviesCrudRazorContext : DbContext
    {
        public MoviesCrudRazorContext(DbContextOptions<MoviesCrudRazorContext> options)
            : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
