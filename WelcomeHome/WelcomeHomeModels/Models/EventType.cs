using System.ComponentModel.DataAnnotations;

namespace WelcomeHomeModels.Models
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}