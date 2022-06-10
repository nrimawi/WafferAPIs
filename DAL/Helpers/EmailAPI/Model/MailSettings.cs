﻿namespace WafferAPIs.DAL.Helpers.EmailAPI.Model
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
    }
}
