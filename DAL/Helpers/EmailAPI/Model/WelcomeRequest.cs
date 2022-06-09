namespace WafferAPIs.DAL.Helpers.EmailAPI.Model
{
    public class WelcomeRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }    
    }
}
