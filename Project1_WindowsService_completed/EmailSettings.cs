namespace Project_WindowsService
{
    public class EmailSettings
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public bool UseAuthentication { get; set; }
    }
}
