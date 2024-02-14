using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthlyViews = table.Column<int>(type: "int", nullable: false),
                    SavedActivity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    AdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Clicks = table.Column<int>(type: "int", nullable: false),
                    TotalViews = table.Column<int>(type: "int", nullable: false),
                    FemaleViews = table.Column<int>(type: "int", nullable: false),
                    MaleViews = table.Column<int>(type: "int", nullable: false),
                    Age16To30Views = table.Column<int>(type: "int", nullable: false),
                    Age31To50Views = table.Column<int>(type: "int", nullable: false),
                    Age50PlusViews = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.AdId);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    CountyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalEvents = table.Column<int>(type: "int", nullable: false),
                    TotalActivities = table.Column<int>(type: "int", nullable: false),
                    TotalActiveUsers = table.Column<int>(type: "int", nullable: false),
                    TotalPeopleAccounts = table.Column<int>(type: "int", nullable: false),
                    TotalEnterpriseAccounts = table.Column<int>(type: "int", nullable: false),
                    TotalAdvertiserAccounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.CountyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Events");
        }
    }
}
