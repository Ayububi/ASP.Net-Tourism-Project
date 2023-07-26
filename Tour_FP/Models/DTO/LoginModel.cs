using System.ComponentModel.DataAnnotations;

namespace Tour_FP.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
