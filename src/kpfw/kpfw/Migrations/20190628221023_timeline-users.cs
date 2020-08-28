using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kpfw.Migrations
{
    public partial class timelineusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timeline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Message = table.Column<string>(maxLength: 300, nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 40, nullable: false),
                    UserEmail = table.Column<string>(maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(maxLength: 250, nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false),
                    TimeZone = table.Column<string>(nullable: false),
                    ShowEmail = table.Column<bool>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    EmailConfirmation = table.Column<Guid>(nullable: true),
                    TwoFactor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timeline");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
