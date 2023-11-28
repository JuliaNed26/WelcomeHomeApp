using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public class SocialPayoutOutDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public ICollection<Guid>? UserCategoriesId { get; set; }

        public ICollection<StepOutDTO>? Steps { get; set; }
    }
}
