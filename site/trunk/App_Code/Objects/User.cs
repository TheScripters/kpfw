using Robo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for User
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
        public bool ShowEmail { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string TwoFactorAuth { get; set; }

        public static (string password, string userName, string displayName, int userId, bool isActive, string TwoFactorAuth)? GetPassword(string login)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT Id, UserPassword, UserName, DisplayName, IsActive, TwoFactor FROM [User] WHERE (UserName = @Login OR UserEmail = @Login) AND IsActive = 1", false))
            {
                cmd.AddIString("@Login", 50, login);
                var r = cmd.ExecuteSingleRowOrNull();
                if (r == null)
                    return null;

                string twoFactor = r["TwoFactor"].ToString();

                return ((string)r["UserPassword"], (string)r["UserName"], r["DisplayName"] as string ?? "", (int)r["Id"], (bool)r["IsActive"], twoFactor);
            }
        }

        public static User GetByLogin(string login)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT Id, UserName, UserEmail, TimeZone, JoinDate, ShowEmail, DisplayName, IsActive, TwoFactor FROM [User] WHERE UserName = @Login OR UserEmail = @Login", false))
            {
                cmd.AddIString("@Login", 50, login);
                var r = cmd.ExecuteSingleRowOrNull();
                if (r == null)
                    return null;

                return new User
                {
                    Id = (int)r["Id"],
                    UserName = (string)r["UserName"],
                    Email = (string)r["UserEmail"],
                    TimeZone = TimeZoneInfo.FindSystemTimeZoneById((string)r["TimeZone"]),
                    JoinDate = (DateTime)r["JoinDate"],
                    ShowEmail = (bool)r["ShowEmail"],
                    DisplayName = r["DisplayName"] as string ?? "",
                    IsActive = (bool)r["IsActive"],
                    TwoFactorAuth = r["TwoFactor"].ToString()
                };
            }
        }

        public static bool NeedsPasswordChanged(string login)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT EmailConfirmation FROM [User] WHERE UserName = @Login", false))
            {
                cmd.AddIString("@Login", 50, login);
                var r = cmd.ExecuteSingleRowOrNull();
                if (r == null)
                    return false;
                return r["EmailConfirmation"] as string != null;
            }
        }

        public static bool LoginExists(string login)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT Id FROM [User] WHERE UserName = @Login", false))
            {
                cmd.AddIString("@Login", 50, login);
                return cmd.ExecuteSingleRowOrNull() != null;
            }
        }

        public static bool EmailExists(string email)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT Id FROM [User] WHERE UserEmail = @Email", false))
            {
                cmd.AddIString("@Email", 50, email);
                return cmd.ExecuteSingleRowOrNull() != null;
            }
        }

        public static bool ConfirmationExists(Guid confirmation)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT Id FROM [User] WHERE EmailConfirmation = @Confirmation", false))
            {
                cmd.AddIGuid("@Confirmation", confirmation);
                return cmd.ExecuteSingleRowOrNull() != null;
            }
        }

        public static void Activate(Guid confirmation)
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE [User] SET IsActive = 1, EmailConfirmation = NULL WHERE EmailConfirmation = @Confirmation", false))
            {
                cmd.AddIGuid("@Confirmation", confirmation);
                cmd.Execute();
            }
        }

        public static void Activate(int id)
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE [User] SET IsActive = 1, EmailConfirmation = NULL WHERE Id = @Id", false))
            {
                cmd.AddIInt("@Id", id);
                cmd.Execute();
            }
        }

        public static string GetUser(Guid confirmation)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT UserName FROM [User] WHERE EmailConfirmation = @Confirmation", false))
            {
                cmd.AddIGuid("@Confirmation", confirmation);
                var r = cmd.ExecuteSingleRowOrNull();
                if (r == null)
                    return null;

                return (string)r["UserName"];
            }
        }

        public static void UpdatePassword(int userId, string hash)
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE [User] SET UserPassword = @Hash WHERE Id = @UserId; UPDATE ResetRequests SET PasswordReset = 1 WHERE UserId = @UserId;", false))
            {
                cmd.AddIInt("@UserId", userId);
                cmd.AddIString("@Hash", 250, hash);
                cmd.Execute();
            }
        }

        public static (bool reset, DateTime requestDate, int userId)? GetResetRequest(Guid resetRequest)
        {
            using (SqlCmd cmd = new SqlCmd("SELECT * FROM ResetRequests WHERE Id = @Id", false))
            {
                cmd.AddIGuid("@Id", resetRequest);
                var r = cmd.ExecuteSingleRowOrNull();
                if (r == null)
                    return null;

                return ((bool)r["PasswordReset"], (DateTime)r["RequestTime"], (int)r["UserId"]);
            }
        }

        public static void AddResetRequest(Guid id, int userId)
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE ResetRequests SET PasswordReset = 1 WHERE UserId = @UserId", false))
            {
                cmd.AddIInt("@UserId", userId);
                cmd.Execute();
            }
            using (SqlCmd cmd = new SqlCmd("INSERT INTO ResetRequests (Id, UserId) VALUES (@Id, @UserId)", false))
            {
                cmd.AddIGuid("@Id", id);
                cmd.AddIInt("@UserId", userId);
                cmd.Execute();
            }
        }

        public void Add(string password, string ip, Guid emailConfirmation)
        {
            using (SqlCmd cmd = new SqlCmd("INSERT INTO [User] (UserName, UserEmail, UserPassword, TimeZone, IPAddress, IsActive, EmailConfirmation) VALUES (@UserName, @UserEmail, @Password, @TimeZone, @IPAddress, 0, @EmailConfirmation); SET @Id = SCOPE_IDENTITY();", false))
            {
                cmd.AddIString("@UserName", 40, UserName);
                cmd.AddIString("@UserEmail", 50, Email);
                cmd.AddIString("@Password", 250, password);
                cmd.AddIString("@TimeZone", 50, TimeZone.Id);
                cmd.AddIString("@IPAddress", 100, ip);
                cmd.AddIGuid("@EmailConfirmation", emailConfirmation);
                cmd.AddOInt("@Id");
                cmd.Execute();
                Id = cmd.GetInt("@Id");
            }
        }

        public static void AssignEmailConfirmation(int userId, Guid? emailConfirmation, bool isActive = false)
        {
            using (SqlCmd cmd = new SqlCmd("UPDATE [User] SET EmailConfirmation = @EmailConfirmation, IsActive = @IsActive WHERE Id = @Id", false))
            {
                cmd.AddIInt("@Id", userId);
                cmd.AddIGuid("@EmailConfirmation", emailConfirmation);
                cmd.AddIBool("@IsActive", isActive);
                cmd.Execute();
            }
        }
    }
}