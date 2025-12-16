namespace Model
{
    public class JWTOptions
    {
        public string SecretKey { get; set; }
        public int TimeoutInMins { get; set; }
    }
}
