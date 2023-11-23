using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeHome.Services.DTO
{
    public class StepOutDTO
    {
        public Guid Id { get; set; }

        public int SequenceNumber { get; set; }
        
        public string Description { get; set; }

        public ICollection<EstablishmentOutDTO>? Establishments { get; set; }

        public ICollection<DocumentOutDTO>? DocumentsBring { get; set; }

        public ICollection<DocumentOutDTO>? DocumentsReceive { get; set; }
    }
}
