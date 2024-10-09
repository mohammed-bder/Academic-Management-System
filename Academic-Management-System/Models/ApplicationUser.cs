using Microsoft.AspNetCore.Identity;

namespace Task02_MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
