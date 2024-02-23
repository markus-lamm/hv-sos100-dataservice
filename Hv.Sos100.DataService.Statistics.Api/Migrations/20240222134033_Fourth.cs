using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Events",
                newName: "EventStatisticsId");

            migrationBuilder.RenameColumn(
                name: "CountyId",
                table: "Counties",
                newName: "CountyStatisticsId");

            migrationBuilder.RenameColumn(
                name: "AdId",
                table: "Ads",
                newName: "AdStatisticsId");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Activities",
                newName: "ActivityStatisticsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventStatisticsId",
                table: "Events",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "CountyStatisticsId",
                table: "Counties",
                newName: "CountyId");

            migrationBuilder.RenameColumn(
                name: "AdStatisticsId",
                table: "Ads",
                newName: "AdId");

            migrationBuilder.RenameColumn(
                name: "ActivityStatisticsId",
                table: "Activities",
                newName: "ActivityId");
        }
    }
}
