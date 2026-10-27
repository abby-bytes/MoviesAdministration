using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Bio { get; set; } = string.Empty;

        // Navigation property back to movies directed by them
        public ICollection<Movie>? Movies { get; set; }
    }
}
