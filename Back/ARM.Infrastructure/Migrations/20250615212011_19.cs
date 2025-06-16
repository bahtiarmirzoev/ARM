using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Brands_BrandId",
                table: "Venues");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Brands_BrandId",
                table: "Venues",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Brands_BrandId",
                table: "Venues");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Brands_BrandId",
                table: "Venues",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
