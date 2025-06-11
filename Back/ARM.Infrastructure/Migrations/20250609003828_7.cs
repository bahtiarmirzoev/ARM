using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AutoServices_AutoServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Specializations",
                table: "AutoServices");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AutoServices_AutoServiceId",
                table: "Services",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AutoServices_AutoServiceId",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "Specializations",
                table: "AutoServices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AutoServices_AutoServiceId",
                table: "Services",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
