namespace Model
{
    public class JwtTokenModel
    {
        public string Token { get; set; }
        public int ExpiersIn { get; set; }
        public string TokenType { get; set; }
    }
}
