using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHomeModels
{
    [Table("Establishments")]
    public class Establishment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public City City { get; set; } = null!;
        public EstablishmentType EstablishmentType { get; set; } = null!;
        public string? PageURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OtherContacts { get; set; }
    }
}
