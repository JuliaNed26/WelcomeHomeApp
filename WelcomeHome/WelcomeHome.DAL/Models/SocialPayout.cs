using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
    public class SocialPayout
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        //why nullable?
        public ICollection<UserCategory>? UserCategories { get; set; }

        public List<PaymentStep> PaymentSteps { get; set; }
    }
}
