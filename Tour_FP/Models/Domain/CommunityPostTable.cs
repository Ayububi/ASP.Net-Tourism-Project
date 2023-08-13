using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tour_FP.Models.Domain
{
    public class CommunityPostTable
    {
        [Key]
        public int PostId { get; set; }

        
        public string? UserId { get; set; }// Add this property to store the user's ID

        [Required]
        public string? Title { get; set; }

        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }

        // List of comments for the post
        public List<CommentTable> Comments { get; set; }

        public string? Images { get; set; }
        [NotMapped]

        public IFormFile? ImageFile { get; set; }

        // Constructor to set default values
        public CommunityPostTable()
        {
            
            CreatedAt = DateTime.Now; // Set the CreatedAt property to the current date and time when a new post is created
            Comments = new List<CommentTable>(); // Initialize the comments list to avoid null reference errors
        }


    }
}
