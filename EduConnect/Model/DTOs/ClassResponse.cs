namespace Model.DTOs
{
    public class ClassResponse
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public List<SectionResponse> Sections {get;set; }
    }
    public class SectionResponse
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string SectionName { get; set; } = string.Empty;
    }

    public class SectionSubjectResponse
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public List<SubjectResponse> Subject { get; set; }
    }
    public class SubjectResponse
    {
        public int SectionSubject { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int? TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
