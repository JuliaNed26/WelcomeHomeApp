namespace WelcomeHome.DAL.Models
{
	public class PaymentStep
	{
		public long SocialPayoutId { get; set; }

		public long StepId { get; set; }

		public int SequenceNumber { get; set; }

		public Step Step { get; set; }

		public SocialPayout SocialPayout { get; set; }
	}
}
