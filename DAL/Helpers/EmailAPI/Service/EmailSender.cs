using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using WafferAPIs.DAL.Helpers.EmailAPI.Model;

namespace WafferAPIs.DAL.Helpers.EmailAPI.Service
{

    public interface IEmailSender
    {
        Task SendEmailAsync(MailReqestData req);
    }
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;

        }


        public async Task SendEmailAsync(MailReqestData req)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\DAL\\Helpers\\EmailAPI\\Service\\Templates\\WelcomeTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();


                MailText = MailText.Replace("[USERNAME]", req.Name).Replace("[PASSWORD]", req.Password).Replace("[LINK]", req.Link);
                MailjetClient client = new MailjetClient(_mailSettings.APIKey, _mailSettings.SecretKey)
                {

                };
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
               .Property(Send.FromEmail, _mailSettings.Mail)
               .Property(Send.FromName, _mailSettings.DisplayName)
               .Property(Send.Subject, req.Subject)
               .Property(Send.HtmlPart, MailText)
               .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", req.ToEmail}
                 }
                   });
                MailjetResponse response = await client.PostAsync(request);
                if (response.StatusCode != 200)
                    throw new Exception(response.GetErrorInfo().ToString());
    }
            catch
            {
                throw;
            }
        }
    }
}