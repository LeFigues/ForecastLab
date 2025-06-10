namespace fl_api.Models.Guides
{
    public class PracticeItem
    {
        public int PracticeNumber { get; set; }
        public string PracticeTitle { get; set; } = null!;
        public List<MaterialItem> Equipment { get; set; } = new();
        public List<MaterialItem> Supplies { get; set; } = new();
        public List<MaterialItem> Reagents { get; set; } = new();
        public int StudentsPerGroup { get; set; }
        public int GroupCount { get; set; }
    }


}
