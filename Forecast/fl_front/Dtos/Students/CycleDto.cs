namespace fl_front.Dtos.Students
{
    public class CycleDto
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }

        public string Display => $"{Year}-{Semester}";
    }
}
