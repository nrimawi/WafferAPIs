namespace WafferAPIs.DAL.Helpers.EmailAPI.Model
{
    public class MailReqestData
    {
        public string ToEmail { get; set; }
        public string Password { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
    }
}
