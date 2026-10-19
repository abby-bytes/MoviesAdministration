using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 stars

        public bool IsPublished { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}