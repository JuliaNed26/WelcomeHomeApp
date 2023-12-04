namespace WelcomeHome.DAL.Models
{
	public class PaymentStep
	{
		public int SocialPayoutId { get; set; }

		public int StepId { get; set; }

		public int SequenceNumber { get; set; }

		public Step Step { get; set; }

		public SocialPayout SocialPayout { get; set; }
	}
}
