using Markdig;
using Markdig.Helpers;
using Robo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for Extensions
    /// </summary>
    public static class Extensions
    {
        public static string ToBase64(this string val)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(val));
        }
        public static string FromBase64(this string val)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(val));
        }
        public static string RenderBBCode(this string val)
        {
            string[] find = { "[b]", "[/b]", "[B]", "[/B]", "[i]", "[/i]", "\\'", "\\\"", "[u]", "[/u]", "[b/]" };
            string[] replace = { "<strong>", "</strong>", "<strong>", "</strong>", "<i>", "</i>", "'", "\"", "<u>", "</u>", "</strong>" };
            //string v = val.Replace("[b]", "<strong>").Replace("[/b]", "</strong>");
            //v = v.Replace("[i]", "<i>").Replace("[/i]", "</i>");
            //v = v.Replace("\\'", "'").Replace("\\\"", "\"");

            return val.Replace(find, replace);
        }
        public static IEnumerable<string> Trim(this IEnumerable<string> val)
        {
            List<string> l = val.ToList();
            for (int i = 0; i< l.Count; i++)
                l[i] = l[i].Trim();

            return l;
        }
        public static string RenderImdbLinks(this string[] vals)
        {
            string val = "";
            foreach (string v in vals)
            {
                DataRow r;
                using (SqlCmd cmd = new SqlCmd("SELECT ImdbNameID FROM CrewLink WHERE CrewName = @CrewMember", false))
                {
                    cmd.AddIString("@CrewMember", 50, v);
                    r = cmd.ExecuteSingleRowOrNull();
                }
                if (r == null)
                    val += $" {v}";
                else
                    val += @" <a href=""https:" + $@"//www.imdb.com/name/nm{r["ImdbNameID"]}/"" target=""_blank"" rel=""nofollow"">{v}</a>";
            }

            return val.Trim();
        }
        public static MarkdownPipelineBuilder UseCaps(this MarkdownPipelineBuilder pipeline)
        {
            OrderedList<IMarkdownExtension> extensions;

            extensions = pipeline.Extensions;

            if (!extensions.Contains<CapsExtension>())
            {
                extensions.Add(new CapsExtension());
            }

            return pipeline;
        }
    }
}