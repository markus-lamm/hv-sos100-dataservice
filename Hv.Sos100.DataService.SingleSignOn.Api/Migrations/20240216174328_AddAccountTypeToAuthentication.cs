using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hv.Sos100.DataService.SingleSignOn.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTypeToAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Authentication",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Authentication");
        }
    }
}
