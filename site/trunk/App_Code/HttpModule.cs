using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for HttpModule
    /// </summary>
    public class HttpModule : IHttpModule
    {
        public void Dispose()
        { }

        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            // modify the "Server" Http Header
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
}