using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public class StepInDTO
    {
        public int SequenceNumber { get; set; }

        public string Description { get; set; }

        public Guid EstablishmentTypeId { get; set; }

        public ICollection<Guid> DocumentsBringId { get; set; }
        
        public ICollection<Guid> DocumentsReceiveId { get; set; }

    }
}
