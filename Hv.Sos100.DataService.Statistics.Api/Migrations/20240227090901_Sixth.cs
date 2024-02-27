using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatatserviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class Sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Rating2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Rating3",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Rating4",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Rating5",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SavedEvents",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TotalAdvertiserAccounts",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountyId",
                table: "Counties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "Ads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CountyId",
                table: "Counties");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "Rating1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating2",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating3",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating4",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating5",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavedEvents",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalAdvertiserAccounts",
                table: "Counties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Activities",
                type: "datetime2",
                nullable: true);
        }
    }
}
