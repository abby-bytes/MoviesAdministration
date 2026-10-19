using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Synopsis { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Rating {  get; set; } = string.Empty;

        [Required]
        public double Runtime { get; set; }

        [Required]
        [StringLength(80)]
        public string Director { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } // Added the '?' so it is not strictly required by default

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

    }
}
