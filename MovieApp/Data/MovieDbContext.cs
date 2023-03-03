using Microsoft.EntityFrameworkCore;
using MovieApp.Data.Models;

namespace MovieApp.Data
{
    public class MovieDbContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public MovieDbContext(DbContextOptions<MovieDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
