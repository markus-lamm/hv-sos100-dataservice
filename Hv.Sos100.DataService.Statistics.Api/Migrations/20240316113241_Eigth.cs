using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Eigth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Ads",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Ads");
        }
    }
}
