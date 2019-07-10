using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace kpfw.Services.Authentication
{
    /// <summary>
    /// Summary description for Authy
    /// </summary>
    public class AuthyClient : IDisposable
    {
        private readonly string _ApiKey;
        private readonly bool _IsTest;

        private const string TestUrlBase = "sandbox-api.authy.com";
        private const string UrlBase = "api.authy.com";

        public AuthyClient(string key, bool isTest = false)
        {
            _ApiKey = key;
            _IsTest = isTest;
        }

        public void Dispose()
        {
            
        }

        private HttpWebRequest BeginRequest(string apiPath, string method)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = (HttpWebRequest)WebRequest.Create(new Uri($"https://{(_IsTest ? TestUrlBase : UrlBase)}/protected/json/{apiPath}"));
            client.Headers.Add("X-Authy-API-Key", _ApiKey);
            client.Method = method;

            return client;
        }

        public string GetAuthenticatorQR(int authyUserId, string userName, string secret)
        {
            var client = BeginRequest($"users/{authyUserId}/{secret}", HttpMethod.POST);
            byte[] data = System.Text.Encoding.UTF8.GetBytes("qr_size=300&label=" + userName);
            client.ContentType = "application/x-www-form-urlencoded";
            client.ContentLength = data.Length;

            using (var stream = client.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)client.GetResponse();

                QRResponse j = JsonConvert.DeserializeObject<QRResponse>(new StreamReader(response.GetResponseStream()).ReadToEnd());

                if (j.Success)
                    return j.QR_Code;
            }
            catch (WebException wEx)
            {
                response = (HttpWebResponse)wEx.Response;
                string error = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }

            return "Failed.";
        }

        public bool VerifyToken(int authyUserId, int token)
        {
            var client = BeginRequest($"verify/{token}/{authyUserId}", HttpMethod.GET);

            try
            {
                var resp = client.GetResponse();
                string res = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                return true;
            }
            catch (Exception ex)
            {
                string res = new StreamReader(((WebException)ex).Response.GetResponseStream()).ReadToEnd();
                return false;
            }
        }

        public void AddUser(string email, string phone, string countryCode)
        {
            var client = BeginRequest("users/new", HttpMethod.POST);
        }
    }
    public class HttpMethod
    {
        public const string GET = "GET";
        public const string POST = "POST";
    }
    public class QRResponse
    {
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
        [JsonProperty("qr_code")]
        public string QR_Code { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}