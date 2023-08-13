using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Tour_FP.Models.Domain
{
    public class CommentTable
    {
        [Key]
        public int CommentId { get; set; }

        public string? UserId { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }

        // Foreign key to relate the comment to a CommunityPost
        public int PostId { get; set; }
        public  CommunityPostTable? CommunityPost { get; set; }
        public CommentTable()
        {
            CreatedAt = DateTime.Now; // Set the CreatedAt property to the current date and time when a new post is created
           
        }
    }
}
