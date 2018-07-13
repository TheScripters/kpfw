using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace kpfw
{
    /// <summary>
    /// Summary description for Sitemap
    /// </summary>
    public class SitemapHandler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            sb.AppendLine(@"<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">");
            context.Response.ContentType = "text/xml";

            string domain = "https://" + "www.kpfanworld.com";

            sb.AppendLine($"<url><loc>{domain}</loc></url>");
            sb.AppendLine($"<url><loc>{domain}/Guides</loc></url>");
            sb.AppendLine($"<url><loc>{domain}/About</loc></url>");
            sb.AppendLine($"<url><loc>{domain}/Contact</loc></url>");
            sb.AppendLine($"<url><loc>{domain}/Chat</loc></url>");
            sb.AppendLine($"<url><loc>{domain}/Caps</loc></url>");
            sb.AppendLine($"<url><loc>{domain}/Donate</loc></url>");

            foreach (var ep in Episode.List())
            {
                sb.AppendLine($"<url><loc>{domain}/Guides/{ep.UrlLabel}</loc></url>");

                if (ep.HasTranscript)
                    sb.AppendLine($"<url><loc>{domain}/Guides/{ep.UrlLabel}/Transcript</loc></url>");
            }

            foreach (var ep in EpCapsList.S1List)
                sb.AppendLine($"<url><loc>{domain}/Caps/{ep.UrlLabel}</loc></url>");

            foreach (var ep in EpCapsList.S2List)
                sb.AppendLine($"<url><loc>{domain}/Caps/{ep.UrlLabel}</loc></url>");

            foreach (var ep in EpCapsList.S3List)
                sb.AppendLine($"<url><loc>{domain}/Caps/{ep.UrlLabel}</loc></url>");

            foreach (var ep in EpCapsList.S4List)
                sb.AppendLine($"<url><loc>{domain}/Caps/{ep.UrlLabel}</loc></url>");

            foreach (var ep in EpCapsList.MovieList)
                sb.AppendLine($"<url><loc>{domain}/Caps/{ep.UrlLabel}</loc></url>");

            foreach (var ep in EpCapsList.MiscList)
                sb.AppendLine($"<url><loc>{domain}/Caps/{ep.UrlLabel}</loc></url>");

            sb.AppendLine("</urlset>");

            context.Response.Write(sb.ToString());
        }
    }
}