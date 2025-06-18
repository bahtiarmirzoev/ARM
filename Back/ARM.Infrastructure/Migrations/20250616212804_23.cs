using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Brands_Latitude_Longitude",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Brands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Brands",
                type: "double precision",
                precision: 10,
                scale: 6,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Brands",
                type: "double precision",
                precision: 10,
                scale: 6,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Latitude_Longitude",
                table: "Brands",
                columns: new[] { "Latitude", "Longitude" });
        }
    }
}
