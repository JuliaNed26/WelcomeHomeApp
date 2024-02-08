using Microsoft.AspNetCore.Identity;

namespace WelcomeHome.DAL.Models
{
    public class User : IdentityUser<long>
    {
        public string FullName { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public Volunteer? Volunteer { get; set; }
    }
}
