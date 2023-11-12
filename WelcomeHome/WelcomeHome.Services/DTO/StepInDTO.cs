using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record StepInDTO
    {
        public string Description { get; set; }

        public Guid EstablishmentTypeId { get; set; }

    }
}
