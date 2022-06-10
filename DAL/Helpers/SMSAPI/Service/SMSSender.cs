

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WafferAPIs.DAL.Helpers.SMSAPI.Model;

namespace WafferAPIs.DAL.Helpers.SMSAPI
{
    public interface ISMSSender
    {
        Task SendSMSAsync(SMSRequestData requestData);

    }



    public class SMSSender : ISMSSender
    {
        private readonly SMSSettings _smsSettings;
        static HttpClient client = new HttpClient();


        public SMSSender(IOptions<SMSSettings> smsSettings)
        {



            _smsSettings = smsSettings.Value;
          

        }



        public async Task SendSMSAsync(SMSRequestData requestData)
        {
            try
            {

                client.BaseAddress = new Uri("https://api.mailjet.com/");
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header


                var json = JsonConvert.SerializeObject(requestData);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v4/sms-send");
                client.DefaultRequestHeaders.Add("Authorization", _smsSettings.Token);
                request.Content = new StringContent(json,
                                                    Encoding.UTF8,
                                                    "application/json");//CONTENT-TYPE header



                var req = await client.SendAsync(request);
                req.EnsureSuccessStatusCode();

                //client.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                //HttpContent context = new StringContent(json, Encoding.UTF8, "application/json");

                //HttpResponseMessage response = await client.PostAsync(
                //    _smsSettings.API_Uri, context);

            }
            catch { throw; }

        }

    }


}
