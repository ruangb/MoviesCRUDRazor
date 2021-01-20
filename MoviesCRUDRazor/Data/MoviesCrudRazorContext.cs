using Microsoft.EntityFrameworkCore;
using MoviesCRUDRazor.Models;

namespace MoviesCRUDRazor.Data
{
    public class MoviesCRUDRazorContext : DbContext
    {
        public MoviesCRUDRazorContext(DbContextOptions<MoviesCRUDRazorContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Integrated Security = SSPI; Persist Security Info=False;User ID = ruan; Initial Catalog = MoviesCrudRazor; Data Source = RUAN\\SQLEXPRESS");
        }
    }
}
