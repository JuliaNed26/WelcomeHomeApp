using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public class EstablishmentTypeOutDTO
    {
        public string Name { get; set; } 
        public ICollection<Establishment>? Establishments { get; set; }
        public ICollection<Step>? Steps { get; set; }
    }
}
