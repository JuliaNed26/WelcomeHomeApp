using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeHome.DAL.Models;

namespace WelcomeHome.Services.DTO
{
    public class DocumentOutDTO
    {
        public string Name { get; set; }
        public ICollection<StepDocument> StepDocuments { get; set; }
    }
}
