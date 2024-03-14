using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropColumn(
                name: "Age16To30Views",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Age31To50Views",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Age50PlusViews",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "FemaleViews",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "MaleViews",
                table: "Ads");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Events",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "Views",
                table: "Events",
                newName: "AgeBelow16Signups");

            migrationBuilder.RenameColumn(
                name: "Age50PlusSignups",
                table: "Events",
                newName: "AgeAbove50Signups");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "EventStatisticsID");

            migrationBuilder.RenameColumn(
                name: "AdvertisementId",
                table: "Ads",
                newName: "AdvertisementID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ads",
                newName: "AdvertisementStatisticsID");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Activities",
                newName: "ActivityID");

            migrationBuilder.RenameColumn(
                name: "SavedActivity",
                table: "Activities",
                newName: "TotalSaved");

            migrationBuilder.RenameColumn(
                name: "MonthlyViews",
                table: "Activities",
                newName: "MaleSaved");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Activities",
                newName: "ActivityStatisticsID");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age16To30Saved",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age31To50Saved",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeAbove50Saved",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeBelow16Saved",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FemaleSaved",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Activities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Age16To30Saved",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Age31To50Saved",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AgeAbove50Saved",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AgeBelow16Saved",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "FemaleSaved",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Events",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "AgeBelow16Signups",
                table: "Events",
                newName: "Views");

            migrationBuilder.RenameColumn(
                name: "AgeAbove50Signups",
                table: "Events",
                newName: "Age50PlusSignups");

            migrationBuilder.RenameColumn(
                name: "EventStatisticsID",
                table: "Events",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AdvertisementID",
                table: "Ads",
                newName: "AdvertisementId");

            migrationBuilder.RenameColumn(
                name: "AdvertisementStatisticsID",
                table: "Ads",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActivityID",
                table: "Activities",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "TotalSaved",
                table: "Activities",
                newName: "SavedActivity");

            migrationBuilder.RenameColumn(
                name: "MaleSaved",
                table: "Activities",
                newName: "MonthlyViews");

            migrationBuilder.RenameColumn(
                name: "ActivityStatisticsID",
                table: "Activities",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Age16To30Views",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age31To50Views",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age50PlusViews",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FemaleViews",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaleViews",
                table: "Ads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyId = table.Column<int>(type: "int", nullable: false),
                    TotalActiveUsers = table.Column<int>(type: "int", nullable: true),
                    TotalActivities = table.Column<int>(type: "int", nullable: true),
                    TotalEnterpriseAccounts = table.Column<int>(type: "int", nullable: true),
                    TotalEvents = table.Column<int>(type: "int", nullable: true),
                    TotalPeopleAccounts = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });
        }
    }
}
