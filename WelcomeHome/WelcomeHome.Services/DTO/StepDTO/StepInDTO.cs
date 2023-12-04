namespace WelcomeHome.Services.DTO
{
    public class StepInDTO
    {
        public int SequenceNumber { get; set; }

        public string Description { get; set; }

        public int EstablishmentTypeId { get; set; }

        public ICollection<int> DocumentsBringId { get; set; }

        public ICollection<int> DocumentsReceiveId { get; set; }

    }
}
