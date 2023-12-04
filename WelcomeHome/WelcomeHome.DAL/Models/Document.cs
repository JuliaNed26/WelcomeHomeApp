using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
	public class Document
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public ICollection<StepDocument> StepDocuments { get; set; }
	}
}

