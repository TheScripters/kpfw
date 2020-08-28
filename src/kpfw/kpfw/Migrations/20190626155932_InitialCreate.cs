using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kpfw.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    UrlLabel = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    AirDate = table.Column<DateTime>(nullable: false),
                    ProductionNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Studio = table.Column<string>(maxLength: 100, nullable: true),
                    Writer = table.Column<string>(maxLength: 300, nullable: true),
                    Director = table.Column<string>(maxLength: 100, nullable: true),
                    Producer = table.Column<string>(maxLength: 100, nullable: true),
                    ExecutiveProducer = table.Column<string>(maxLength: 200, nullable: true),
                    Stars = table.Column<string>(maxLength: 300, nullable: true),
                    GuestStars = table.Column<string>(maxLength: 300, nullable: true),
                    Recap = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Transcript = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episode");
        }
    }
}
