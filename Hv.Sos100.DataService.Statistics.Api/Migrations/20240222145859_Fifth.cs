using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventStatisticsId",
                table: "Events",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CountyStatisticsId",
                table: "Counties",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AdStatisticsId",
                table: "Ads",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActivityStatisticsId",
                table: "Activities",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Counties",
                newName: "CountyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ads",
                newName: "AdId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Activities",
                newName: "ActivityId");
        }
    }
}
