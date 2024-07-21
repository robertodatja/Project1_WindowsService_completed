namespace Project_WindowsService
{
    public class EmailData
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachments { get; set; }
        public Dictionary<string, string> CCs { get; set; }
    }
}
