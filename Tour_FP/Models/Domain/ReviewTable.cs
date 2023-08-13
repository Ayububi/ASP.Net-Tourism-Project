using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tour_FP.Models.Domain
{
    public class ReviewTable
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }


        [Required]
        [MaxLength(500)]
        public string? ReviewText { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Foreign key 

        public string? UserId { get; set; } // Add this property to store the user's ID

        public string? UserName { get; set; }

        public int DestinationId { get; set; }
        public Admin_Dashboard? Admin_Dashboard { get; set; }


        public ReviewTable()
        {
            CreatedAt = DateTime.Now; // Set the CreatedAt property to the current date and time
        }

    }
}
