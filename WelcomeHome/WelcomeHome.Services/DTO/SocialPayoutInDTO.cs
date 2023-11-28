using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public class SocialPayoutInDTO
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public ICollection<Guid> UserCategoriesId { get; set; }

        public ICollection<StepInDTO>? NewPaymentSteps { get; set; }

        public ICollection<ExistingStepInDTO>? ExistingPaymentSteps { get; set; }
    }
}
