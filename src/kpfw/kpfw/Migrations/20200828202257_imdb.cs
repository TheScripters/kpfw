using Microsoft.EntityFrameworkCore.Migrations;

namespace kpfw.Migrations
{
    public partial class imdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrewLink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrewName = table.Column<string>(maxLength: 50, nullable: false),
                    ImdbNameID = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewLink", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewLink_CrewName",
                table: "CrewLink",
                column: "CrewName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrewLink");
        }
    }
}
