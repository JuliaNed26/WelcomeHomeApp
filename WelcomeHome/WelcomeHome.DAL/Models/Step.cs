using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
	public class Step
	{
		[Key]
		public Guid Id { get; set; }

		public string Description { get; set; }

		public Guid EstablishmentTypeId { get; set; }

		public EstablishmentType EstablishmentType { get; set; }

		public List<StepDocument> StepDocuments { get; set; }

		public ICollection<PaymentStep> PaymentSteps { get; set; }
	}
}
