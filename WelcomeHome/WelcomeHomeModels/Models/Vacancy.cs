using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHomeModels.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Salary { get; set; }
        public Establishment Establishment { get; set; } = null!;
        public string PageURL { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? OtherContacts { get; set; }

    }
}
