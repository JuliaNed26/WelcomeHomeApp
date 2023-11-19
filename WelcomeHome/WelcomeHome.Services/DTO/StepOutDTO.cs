using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public record StepOutDTO
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public EstablishmentType EstablishmentType { get; set; }

        public ICollection<StepDocument> StepDocuments { get; set; }

        public ICollection<PaymentStep> PaymentSteps { get; set; }

    }
}
