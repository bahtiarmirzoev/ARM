using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntityVenueEntity_Services_ServicesId",
                table: "ServiceEntityVenueEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceEntityVenueEntity_Venues_VenuesId",
                table: "ServiceEntityVenueEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceEntityVenueEntity",
                table: "ServiceEntityVenueEntity");

            migrationBuilder.RenameTable(
                name: "ServiceEntityVenueEntity",
                newName: "VenueServices");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceEntityVenueEntity_VenuesId",
                table: "VenueServices",
                newName: "IX_VenueServices_VenuesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VenueServices",
                table: "VenueServices",
                columns: new[] { "ServicesId", "VenuesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VenueServices_Services_ServicesId",
                table: "VenueServices",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VenueServices_Venues_VenuesId",
                table: "VenueServices",
                column: "VenuesId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VenueServices_Services_ServicesId",
                table: "VenueServices");

            migrationBuilder.DropForeignKey(
                name: "FK_VenueServices_Venues_VenuesId",
                table: "VenueServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VenueServices",
                table: "VenueServices");

            migrationBuilder.RenameTable(
                name: "VenueServices",
                newName: "ServiceEntityVenueEntity");

            migrationBuilder.RenameIndex(
                name: "IX_VenueServices_VenuesId",
                table: "ServiceEntityVenueEntity",
                newName: "IX_ServiceEntityVenueEntity_VenuesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceEntityVenueEntity",
                table: "ServiceEntityVenueEntity",
                columns: new[] { "ServicesId", "VenuesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntityVenueEntity_Services_ServicesId",
                table: "ServiceEntityVenueEntity",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceEntityVenueEntity_Venues_VenuesId",
                table: "ServiceEntityVenueEntity",
                column: "VenuesId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
