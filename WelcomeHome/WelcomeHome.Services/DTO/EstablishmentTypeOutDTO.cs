using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record EstablishmentTypeOutDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } 
        public ICollection<Establishment>? Establishments { get; init; }
        public ICollection<Step>? Steps { get; init; }
    }
}
