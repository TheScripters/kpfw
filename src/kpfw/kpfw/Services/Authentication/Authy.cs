using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private readonly HttpClient httpClient;

        public AuthyClient(string key, bool isTest = false)
        {
            _ApiKey = key;
            _IsTest = isTest;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Authy-API-Key", key);
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public string GetAuthenticatorQR(int authyUserId, string userName, string secret)
        {
            try
            {
                using (var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "qr_size", "300" },
                    { "label", userName }
                }))
                using (HttpResponseMessage response = httpClient.PostAsync($"https://{(_IsTest ? TestUrlBase : UrlBase)}/protected/json/users/{authyUserId}/{secret}", content).Result)
                {
                    response.EnsureSuccessStatusCode();
                    using (HttpContent resp = response.Content)
                    {
                        string result = resp.ReadAsStringAsync().Result;
                        QRResponse j = JsonConvert.DeserializeObject<QRResponse>(result);
                        if (j.Success)
                            return j.QR_Code;
                    }
                }
            }
            catch
            { }

            return "Failed.";
        }

        public bool VerifyToken(int authyUserId, int token)
        {
            //var client = BeginRequest($"verify/{token}/{authyUserId}", HttpMethod.GET);

            try
            {
                using (HttpResponseMessage response = httpClient.GetAsync($"https://{(_IsTest ? TestUrlBase : UrlBase)}/protected/json/verify/{token}/{authyUserId}").Result)
                {
                    response.EnsureSuccessStatusCode();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddUser(string email, string phone, string countryCode)
        {
            //var client = BeginRequest("users/new", HttpMethod.POST);
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