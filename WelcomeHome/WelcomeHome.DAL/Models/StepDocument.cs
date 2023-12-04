namespace WelcomeHome.DAL.Models
{
	public class StepDocument
	{
		public int StepId { get; set; }

		public int DocumentId { get; set; }

		public bool ToReceive { get; set; }

		public Step Step { get; set; }

		public Document Document { get; set; }
	}
}