﻿

using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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


        //public async Task SendSMSAsync(SMSRequestData requestData)
        //{


        //    try
        //    {
        //        var txtResult = (string)null;

        //        using (var client = new HttpClient())
        //        {

        //            var uriBuilder = new UriBuilder("https://api.mailjet.com/");
        //            uriBuilder.Path = "v4/sms-send";

        //            client.BaseAddress = new Uri("https://api.mailjet.com/");
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Add("Authorization", _smsSettings.Token);



        //            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v4/sms-send");


        //            var json = JsonConvert.SerializeObject(requestData);
        //            request.Content = new StringContent(json,
        //                                                   Encoding.UTF8,
        //                                                "application/json");//CONTENT-TYPE header

        //            // sending the request
        //            var response = await client.SendAsync(request);

        //            // check results if success
        //            if (response.IsSuccessStatusCode)
        //            {

        //                txtResult = response.Content.ReadAsStringAsync().Result;
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}


        public async Task SendSMSAsync(SMSRequestData requestData)
        {
            try
            {

                client.BaseAddress = new Uri("https://api.mailjet.com/");
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                client.DefaultRequestHeaders.Add("Authorization", _smsSettings.Token);


                var json = JsonConvert.SerializeObject(requestData);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v4/sms-send");
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






