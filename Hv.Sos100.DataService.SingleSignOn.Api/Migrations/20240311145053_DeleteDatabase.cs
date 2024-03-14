using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hv.Sos100.DataService.SingleSignOn.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authentication");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    AuthenticationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => x.AuthenticationID);
                });
        }
    }
}
