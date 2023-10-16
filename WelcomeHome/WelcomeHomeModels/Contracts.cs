using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeHomeModels
{
    [Table("Contracts")]
    public class Contract
	{
		[Key]
		public int Id { get; set; }
		public Volunteer Volunteer { get; set; } = null!;
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public string URL { get; set; }

	}
}