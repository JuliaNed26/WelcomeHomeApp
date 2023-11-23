namespace WelcomeHome.DAL.Models
{
	public class StepDocument
	{
		public Guid StepId { get; set; }

		public Guid DocumentId { get; set; }

		public bool ToReceive { get; set; }

		public Step Step { get; set; }

		public Document Document { get; set; }
	}
}