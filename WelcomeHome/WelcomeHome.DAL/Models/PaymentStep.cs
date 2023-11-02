namespace WelcomeHome.DAL.Models
{
	public class PaymentStep
	{
		public Guid SocialPayoutId { get; set; }

		public Guid StepId { get; set; }

		public int SequenceNumber { get; set; }

		public Step Step { get; set; }

		public SocialPayout SocialPayout { get; set; }
	}
}
