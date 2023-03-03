using System.ComponentModel.DataAnnotations;

namespace MovieApp.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public double Budget { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}
