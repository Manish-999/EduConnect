namespace Model.DTOs
{
    public class ClassDTO
    {
        public int SchoolId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int? ClassTeacher { get; set; }
        public List<string> SectionName { get; set; }
    }

    // SectionDTO.cs
    public class SectionDTO
    {
        public int ClassId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public List<int> SubjectIds { get; set; } = new List<int>(); // subjects for this section
    }

    // SubjectDTO.cs
    public class SubjectDTO
    {
        public int SchoolId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
    }
    public class UpdateSection
    {
        public int SchoolId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
    }
}

