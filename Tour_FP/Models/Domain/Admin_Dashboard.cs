using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tour_FP.Models.Domain
{
    public class Admin_Dashboard
    {
        [Key]
        public int DestinationId { get; set; }

        [Required]
        public string? CountryName { get; set; }

        [Required]
        public string? Location { get; set; }

        
        public string? Images { get; set; }
        public string? Description { get; set; }
        
        
        //Package Data 
        public string? Package_Title { get; set; }

        [Required]
        public int? Price { get; set; }

        public string? Details { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfPersons { get; set; }

        [NotMapped]
        
        public IFormFile? ImageFile { get; set; }

    }
}
