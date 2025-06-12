using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_Brands_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_Brands_AutoServiceId",
                table: "WorkingHours",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_Brands_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_Brands_AutoServiceId",
                table: "WorkingHours",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
