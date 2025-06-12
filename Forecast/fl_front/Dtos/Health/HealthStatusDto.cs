namespace fl_front.Dtos.Health
{
    public class HealthStatusDto
    {
        public bool Mongo { get; set; }
        public bool OpenAI { get; set; }
        public bool StudentsApi { get; set; }
        public bool LabsApi { get; set; }
        public bool PdfStorage { get; set; }
    }
}
