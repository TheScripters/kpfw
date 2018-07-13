using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for LogHistory
    /// </summary>
    public class LogHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public LogType Type { get; set; }
        public ActionType Action { get; set; }
        public DateTime DateLogged { get; set; }
        public string PreviousChange { get; set; }
        public string NewChange { get; set; }

        public LogHistory()
        {
            NewChange = "";
            PreviousChange = "";
        }

        public void Add()
        {
            using (SqlCmd cmd = new SqlCmd("INSERT INTO LogHistory (UserId, [Type], ActionType, PreviousChange, NewChange) VALUES (@UserId, @Type, @Action, @Previous, @New)", false))
            {
                cmd.AddIInt("@UserId", UserId);
                cmd.AddIInt("@Type", (int)Type);
                cmd.AddIInt("@Action", (int)Action);
                cmd.AddIString("@Previous", -1, PreviousChange);
                cmd.AddIString("@New", -1, NewChange);
                cmd.Execute();
            }
        }
    }

    public enum LogType
    {
        Cast,
        Crew,
        Cultural,
        Description,
        Goof,
        GuestCast,
        Note,
        Page,
        Quote,
        Recap,
        Transcript
    }

    public enum ActionType
    {
        Changed = 0,
        Added = 1,
        Deleted = 2
    }
}