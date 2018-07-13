using Robo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for Timeline
    /// </summary>
    public class Timeline
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }

        public Timeline()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<Timeline> ListToday()
        {
            List<Timeline> l = new List<Timeline>();
            DataTable t;
            using (SqlCmd cmd = new SqlCmd("SELECT * FROM Timeline WHERE MONTH(GETUTCDATE()) = MONTH([Date]) AND DAY(GETUTCDATE()) = DAY([Date])", false))
            {
                t = cmd.ExecuteTable();
            }

            foreach (DataRow r in t.Rows)
            {
                l.Add(new Timeline
                {
                    Id = (int)r["Id"],
                    Date = (DateTime)r["Date"],
                    Message = (string)r["Message"],
                    UserId = (int)r["UserId"]
                });
            }

            return l;
        }
    }
}