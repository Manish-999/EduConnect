namespace Model.DTOs
{
    public class CreateClassRequest
    {
        public string ClassName { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public int? ClassTeacherId { get; set; }
    }
}

