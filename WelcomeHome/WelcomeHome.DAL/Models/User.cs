using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.DAL.Models
{
    public class User : IdentityUser<int>
    {
        public string FullName {  get; set; }
		public RefreshToken RefreshToken { get; set; }
		public Volunteer? Volunteer { get; set;}
    }
}
