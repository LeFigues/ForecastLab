namespace fl_api.Dtos.StudentsApi
{
    public class SubjectDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string CareerId { get; set; } = null!;
        public int Semester { get; set; }
    }
}
