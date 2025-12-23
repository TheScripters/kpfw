using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kpfw.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalLoginFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "User",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "ExternalLoginProvider",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalLoginProviderKey",
                table: "User",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ExternalLoginProvider_ExternalLoginProviderKey",
                table: "User",
                columns: new[] { "ExternalLoginProvider", "ExternalLoginProviderKey" },
                unique: true,
                filter: "[ExternalLoginProvider] IS NOT NULL AND [ExternalLoginProviderKey] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_ExternalLoginProvider_ExternalLoginProviderKey",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ExternalLoginProvider",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ExternalLoginProviderKey",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "User",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
