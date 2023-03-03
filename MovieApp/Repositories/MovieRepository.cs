using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Data.Models;
using MovieApp.DTOs;

namespace MovieApp.Repositories
{
    public class MovieRepository:IMovieRepository
    {
        private readonly MovieDbContext _movieDbContext;

        public MovieRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public async Task<List<Movie>> GetMovies()
        {
            return await _movieDbContext.Movies.ToListAsync();
        }
        public async Task<Movie> GetMovie(int id)
        {
            return await _movieDbContext.Movies.FindAsync(id);
        }
        public async Task<Movie> AddMovie(MovieDTO movieDTO)
        {
            Movie movie = new Movie();
            movie.Title = movieDTO.Title;
            movie.Budget = movieDTO.Budget;
            movie.Description = movieDTO.Description;
            movie.Genre=movieDTO.Genre;
            await _movieDbContext.Movies.AddAsync(movie);
            await _movieDbContext.SaveChangesAsync();
            return movie;
        }
        public async Task<CustomReturnDTO> UpdateMovie(int id, MovieDTO movieDTO)
        {
            var movie = await _movieDbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return new CustomReturnDTO { Type = "BAD", Message = "Movie with this id could not found!" };
            }
            movie.Title = movieDTO.Title;
            movie.Budget = movieDTO.Budget;
            movie.Description = movieDTO.Description;
            movie.Genre = movieDTO.Genre;
            await _movieDbContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = "OK", Message = "Movie successfully edited!" };
        }
        public async Task<CustomReturnDTO> DeleteMovie(int id)
        {
            var movie = await _movieDbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return new CustomReturnDTO { Type = "BAD", Message = "Movie with this id could not found!" };
            }
            _movieDbContext.Movies.Remove(movie);
            await _movieDbContext.SaveChangesAsync();
            return new CustomReturnDTO { Type = "OK", Message = "Movie successfully deleted!" };
        }
    }
}
