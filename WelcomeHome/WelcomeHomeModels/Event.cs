using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHomeModels
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Establishment? Establishment { get; set; }
        public EventType? EventType { get; set; }
        public Volunteer Volunteer { get; set; } = null!;
    }
}
