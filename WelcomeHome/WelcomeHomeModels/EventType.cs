using System.ComponentModel.DataAnnotations;

namespace WelcomeHomeModels
{
    public class EventType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}