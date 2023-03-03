using MovieApp.Data.Models;
using MovieApp.DTOs;

namespace MovieApp.Repositories
{
    public interface IMovieRepository
    {
        public Task<List<Movie>> GetMovies();
        public Task<Movie> GetMovie(int id);
        public Task<Movie> AddMovie(MovieDTO movieDTO);
        public Task<CustomReturnDTO> UpdateMovie(int id, MovieDTO movieDTO);
        public Task<CustomReturnDTO> DeleteMovie(int id);
    }
}
