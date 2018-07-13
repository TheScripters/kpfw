using Robo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for ContentPage
    /// </summary>
    public class ContentPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }

        public ContentPage()
        {
            Name = "";
            Path = "";
            Content = "";
        }

        public static ContentPage GetByUrl(string path)
        {
            DataRow r;
            using (SqlCmd cmd = new SqlCmd("SELECT * FROM ContentPage WHERE [Path] = @Path", false))
            {
                cmd.AddIString("@Path", 100, path);
                r = cmd.ExecuteSingleRowOrNull();
            }
            if (r == null)
                return null;

            return new ContentPage
            {
                Id = (int)r["Id"],
                Name = (string)r["Name"],
                Path = (string)r["Path"],
                Content = (string)r["Content"]
            };
        }

        public static ContentPage Get(int id)
        {
            DataRow r;
            using (SqlCmd cmd = new SqlCmd("SELECT * FROM ContentPage WHERE Id = @Id", false))
            {
                cmd.AddIInt("@Id", id);
                r = cmd.ExecuteSingleRowOrNull();
            }
            if (r == null)
                return null;

            return new ContentPage
            {
                Id = (int)r["Id"],
                Name = (string)r["Name"],
                Path = (string)r["Path"],
                Content = (string)r["Content"]
            };
        }

        public static List<ContentPage> List()
        {
            List<ContentPage> l = new List<ContentPage>();
            DataTable t;
            using (SqlCmd cmd = new SqlCmd("SELECT * FROM ContentPage ORDER BY [Name]", false))
            {
                t = cmd.ExecuteTable();
            }
            foreach (DataRow r in t.Rows)
                l.Add(new ContentPage
                {
                    Id = (int)r["Id"],
                    Name = (string)r["Name"],
                    Path = (string)r["Path"],
                    Content = (string)r["Content"]
                });

            return l;
        }

        public void Add()
        {
            using (SqlCmd cmd = new SqlCmd("INSERT INTO ContentPage VALUES (@Name, @Path, @Content) SET @Id = SCOPE_IDENTITY()", false))
            {
                cmd.AddIString("@Name", 100, Name);
                cmd.AddIString("@Path", 100, Path);
                cmd.AddIString("@Content", -1, Content);
                cmd.AddOInt("@Id");
                cmd.Execute();
                Id = cmd.GetInt("@Id");
            }
        }

        public void Set()
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE ContentPage SET [Name] = @Name, [Path] = @Path, Content = @Content WHERE Id = @Id", false))
            {
                cmd.AddIInt("@Id", Id);
                cmd.AddIString("@Name", 100, Name);
                cmd.AddIString("@Path", 100, Path);
                cmd.AddIString("@Content", -1, Content);
                cmd.Execute();
            }
        }
    }
}