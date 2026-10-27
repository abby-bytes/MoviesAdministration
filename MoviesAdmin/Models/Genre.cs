using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Genre

    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string GenreName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Movie>? Movies { get; set; }
    }
}
