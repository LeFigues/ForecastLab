namespace fl_api.Dtos.StudentsApi
{
    public class GroupDto
    {
        public string GroupId { get; set; } = null!;
        public int StudentCount { get; set; }

        public string Subject { get; set; } = null!;
        public string Teacher { get; set; } = null!;

        public GroupCycleDto Cycle { get; set; } = null!;
    }
}
