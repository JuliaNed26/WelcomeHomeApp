namespace WelcomeHome.Services.DTO
{
    public class StepOutDTO
    {
        public int Id { get; set; }

        //public int SequenceNumber { get; set; }

        public string Description { get; set; }

        public ICollection<EstablishmentFullInfoDTO>? Establishments { get; set; }

        public ICollection<DocumentOutDTO>? DocumentsBring { get; set; }

        public ICollection<DocumentOutDTO>? DocumentsReceive { get; set; }
    }
}
