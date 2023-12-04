using Microsoft.AspNetCore.Identity;

namespace WelcomeHome.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public Volunteer? Volunteer { get; set; }
    }
}
