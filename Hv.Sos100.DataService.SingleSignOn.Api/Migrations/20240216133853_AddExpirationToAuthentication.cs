using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hv.Sos100.DataService.SingleSignOn.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddExpirationToAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiration",
                table: "Authentication",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenExpiration",
                table: "Authentication");
        }
    }
}
