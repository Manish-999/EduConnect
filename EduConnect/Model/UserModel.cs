namespace Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public int UserRole { get; set; }
        /// <summary>School id for school admin (role 2).</summary>
        public int? SchoolId { get; set; }
        /// <summary>Minimal school profile for school admin context.</summary>
        public object? School { get; set; }
    }
}
