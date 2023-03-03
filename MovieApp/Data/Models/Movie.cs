using System.ComponentModel.DataAnnotations;

namespace MovieApp.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        public double Budget { get; set; }
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
    }
}