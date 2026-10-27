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
        [StringLength(20)]
        public string Rating { get; set; } = string.Empty;

        [Required]
        public double Runtime { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Foreign Keys & Navigation Properties

        // Foreign Key for Director
        [Display(Name = "Director")]
        public int DirectorId { get; set; }
        public Director? Director { get; set; } // Navigation Property

        // Foreign Key for Genre
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; } // Navigation Property
    }
}