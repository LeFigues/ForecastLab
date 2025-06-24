namespace fl_api.Dtos.StudentsApi
{
    public class CareerDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FacultyId { get; set; } = null!;
        public int TotalSemesters { get; set; }
    }
}
