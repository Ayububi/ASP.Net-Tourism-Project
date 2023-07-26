using Microsoft.AspNetCore.Identity;

namespace Tour_FP.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        

    }
}
