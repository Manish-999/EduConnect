namespace Model.DTOs
{
    public class ClassSubjectResponseDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int? TeacherId { get; set; }
    }
}

