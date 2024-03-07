using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
	public class Document
	{
		[Key]
		public long Id { get; set; }

		public string Name { get; set; }

		public ICollection<StepDocument> StepDocuments { get; set; }
	}
}

