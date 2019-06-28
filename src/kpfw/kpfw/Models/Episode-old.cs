using kpfw.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for Episode
    /// </summary>
    public class EpisodeOld
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string UrlLabel { get; set; }
        public string Description { get; set; }
        public DateTime AirDate { get; set; }
        public string ProductionNumber { get; set; }
        public string Studio { get; set; }
        public string[] Writer { get; set; }
        public string[] Director { get; set; }
        public string[] Producer { get; set; }
        public string[] ExecutiveProducer { get; set; }
        public string[] Stars { get; set; }
        public string[] GuestStars { get; set; }
        public string Recap { get; set; }
        public string Transcript { get; set; }
        public int Season { get { return Convert.ToInt32(ProductionNumber[0].ToString()); } }
        public bool HasTranscript { get; set; }

        public EpisodeOld()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<EpisodeOld> List(int? season = null)
        {
            List<EpisodeOld> list = new List<EpisodeOld>();
            DataTable t = new DataTable();
            //using (SqlCmd cmd = new SqlCmd("SELECT Id, Number, Title, AirDate, ProductionNumber, UrlLabel, CASE WHEN LEN(Transcript) > 10 THEN CAST(1 as bit) ELSE CAST(0 as bit) END as HasTranscript FROM Episode WHERE @Season IS NULL OR ProductionNumber LIKE @Season + '-%'", false))
            //{
            //    cmd.AddIString("@Season", 10, season?.ToString());
            //    t = cmd.ExecuteTable();
            //}

            foreach (DataRow r in t.Rows)
            {
                list.Add(new EpisodeOld()
                {
                    Id = (int)r["Id"],
                    Number = (int)r["Number"],
                    Title = (string)r["Title"],
                    AirDate = (DateTime)r["AirDate"],
                    ProductionNumber = (string)r["ProductionNumber"],
                    UrlLabel = (string)r["UrlLabel"],
                    HasTranscript = (bool)r["HasTranscript"]
                });
            }

            return list;
        }

        public static EpisodeOld GetByUrl(string url)
        {
            DataRow r = new DataTable().NewRow();
            //using (SqlCmd cmd = new SqlCmd("SELECT * FROM Episode WHERE UrlLabel = @Url", false))
            //{
            //    cmd.AddIString("@Url", 100, url);
            //    r = cmd.ExecuteSingleRowOrNull();
            //}
            if (r == null)
                return null;

            return new EpisodeOld()
            {
                Id = (int)r["Id"],
                Number = (int)r["Number"],
                Title = (string)r["Title"],
                AirDate = (DateTime)r["AirDate"],
                ProductionNumber = (string)r["ProductionNumber"],
                Stars = ((string)r["Starring"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray(),
                GuestStars = ((string)r["GuestStarring"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray(),
                Producer = ((string)r["Producer"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray(),
                ExecutiveProducer = ((string)r["ExecProducer"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray(),
                Description = (string)r["Description"],
                Writer = ((string)r["Writer"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray(),
                Studio = (string)r["Studio"],
                UrlLabel = (string)r["UrlLabel"],
                Director = ((string)r["Director"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Trim().ToArray(),
                Recap = r["Recap"].ToString(),
                Transcript = r["Transcript"].ToString()
            };
        }

        public static string GetUrl(int id)
        {
            DataRow r = new DataTable().NewRow();
            //using (SqlCmd cmd = new SqlCmd("SELECT UrlLabel FROM Episode WHERE Id = @Id", false))
            //{
            //    cmd.AddIInt("@Id", id);
            //    r = cmd.ExecuteSingleRowOrNull();
            //}
            if (r == null)
                return "";

            return r["UrlLabel"].ToString();
        }

        public static DataTable ListNotes(int id)
        {
            return new DataTable();
            //using (SqlCmd cmd = new SqlCmd("SELECT Note FROM Notes WHERE EpisodeId = @Id", false))
            //{
            //    cmd.AddIInt("@Id", id);
            //    return cmd.ExecuteTable();
            //}
        }

        public static DataTable ListQuotes(int id)
        {
            return new DataTable();
            //using (SqlCmd cmd = new SqlCmd("SELECT QuoteText FROM Quote WHERE EpisodeId = @Id", false))
            //{
            //    cmd.AddIInt("@Id", id);
            //    return cmd.ExecuteTable();
            //}
        }

        public static DataTable ListGoofs(int id)
        {
            return new DataTable();
            //using (SqlCmd cmd = new SqlCmd("SELECT GoofText FROM Goofs WHERE EpisodeId = @Id", false))
            //{
            //    cmd.AddIInt("@Id", id);
            //    return cmd.ExecuteTable();
            //}
        }

        public static DataTable ListCultural(int id)
        {
            return new DataTable();
            //using (SqlCmd cmd = new SqlCmd("SELECT CulturalText FROM Cultural WHERE EpisodeId = @Id", false))
            //{
            //    cmd.AddIInt("@Id", id);
            //    return cmd.ExecuteTable();
            //}
        }

        public static string ParseBBCode(string value)
        {
            //value = value.Replace("[b]", "<strong>").Replace("[/b]", "</strong>");

            return value.RenderBBCode();
        }
    }
    public class EpisodeTitle
    {
        public string Title;
        public string UrlLabel;
        public string CDNPath;
        public int Count;

        public EpisodeTitle()
        {
            Count = -1;
        }
    }
}