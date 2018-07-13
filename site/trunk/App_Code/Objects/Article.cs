using Robo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for Article
    /// </summary>
    public class Article
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public string Title { get; set; } = "";
        public string SubHeading { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime PublishDate { get; set; }
        public string UrlLabel { get; set; } = "";
        public string Url { get { return $"{PublishDate:yyyy/MM}/{UrlLabel}"; } }
        public string AuthorName { get { return Author.DisplayName; } }

        public Article()
        {
            Author = new User();
        }

        public void Add()
        {
            using (SqlCmd cmd = new SqlCmd("INSERT INTO Article VALUES (@UserId, @Title, @SubHeading, @ShortDescription, @Content, @PublishDate, @UrlLabel); SET @Id = SCOPE_IDENTITY();", false))
            {
                cmd.AddIInt("@UserId", Author.Id);
                cmd.AddIString("@Title", 300, Title);
                cmd.AddIString("@SubHeading", 300, SubHeading);
                cmd.AddIString("@ShortDescription", 1000, ShortDescription);
                cmd.AddIString("@Content", -1, Content);
                cmd.AddIDateTime("@PublishDate", PublishDate);
                cmd.AddIString("@UrlLabel", 300, CreateUrl());
                cmd.AddOInt("@Id");
                cmd.Execute();
                Id = cmd.GetInt("@Id");
            }
        }

        public void Set()
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE Article SET Title = @Title, SubHeading = @SubHeading, ShortDescription = @ShortDescription, Content = @Content, PublishDate = @PublishDate, UrlLabel = @UrlLabel WHERE Id = @Id", false))
            {
                cmd.AddIInt("@Id", Id);
                cmd.AddIString("@Title", 300, Title);
                cmd.AddIString("@SubHeading", 300, SubHeading);
                cmd.AddIString("@ShortDescription", 1000, ShortDescription);
                cmd.AddIString("@Content", -1, Content);
                cmd.AddIDateTime("@PublishDate", PublishDate);
                cmd.AddIString("@UrlLabel", 300, CreateUrl());
                cmd.Execute();
            }
        }

        public static List<Article> List()
        {
            List<Article> l = new List<Article>();

            return l;
        }

        private bool UrlExists()
        {
            DataRow r;
            using (SqlCmd cmd = new SqlCmd("SELECT Id FROM Article WHERE UrlLabel = @UrlLabel AND MONTH(PublishDate) = MONTH(@Date) AND YEAR(PublishDate) = YEAR(@Date)", false))
            {
                cmd.AddIString("@UrlLabel", 300, UrlLabel);
                cmd.AddIDateTime("@Date", PublishDate);
                r = cmd.ExecuteSingleRowOrNull();
            }
            if (r == null)
                return false;

            return (int)r["Id"] != Id;
        }

        private string CreateUrl()
        {
            string url = Regex.Replace(Title, "[^0-9a-zA-Z-_/ ]", "", RegexOptions.Compiled);
            url = Regex.Replace(url, "[_/ ]", "-", RegexOptions.Compiled);

            while (url.Contains("--"))
                url = url.Replace("--", "-");

            UrlLabel = url;
            int attempt = 1;
            while (UrlExists())
            {
                UrlLabel = url + "-" + attempt;
                attempt++;
            }

            return UrlLabel;
        }
    }
}