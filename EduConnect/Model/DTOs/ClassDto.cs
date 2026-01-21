namespace Model.DTOs
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public int? ClassTeacherId { get; set; }
    }
}

