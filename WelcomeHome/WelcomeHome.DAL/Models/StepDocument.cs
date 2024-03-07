namespace WelcomeHome.DAL.Models
{
	public class StepDocument
	{
		public long StepId { get; set; }

		public long DocumentId { get; set; }

		public bool ToReceive { get; set; }

		public Step Step { get; set; }

		public Document Document { get; set; }
	}
}