using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Brands_AutoServiceId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Venues_VenueId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "BrandEntityServiceEntity");

            migrationBuilder.DropIndex(
                name: "IX_Services_AutoServiceId_Name",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_VenueId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "AutoServiceId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "Services",
                newName: "BrandId");

            migrationBuilder.CreateTable(
                name: "ServiceEntityVenueEntity",
                columns: table => new
                {
                    ServicesId = table.Column<string>(type: "character varying(24)", nullable: false),
                    VenuesId = table.Column<string>(type: "character varying(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntityVenueEntity", x => new { x.ServicesId, x.VenuesId });
                    table.ForeignKey(
                        name: "FK_ServiceEntityVenueEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceEntityVenueEntity_Venues_VenuesId",
                        column: x => x.VenuesId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_BrandId_Name",
                table: "Services",
                columns: new[] { "BrandId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntityVenueEntity_VenuesId",
                table: "ServiceEntityVenueEntity",
                column: "VenuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Brands_BrandId",
                table: "Services",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Brands_BrandId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "ServiceEntityVenueEntity");

            migrationBuilder.DropIndex(
                name: "IX_Services_BrandId_Name",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Services",
                newName: "VenueId");

            migrationBuilder.AddColumn<string>(
                name: "AutoServiceId",
                table: "Services",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BrandEntityServiceEntity",
                columns: table => new
                {
                    AutoRepairsId = table.Column<string>(type: "character varying(24)", nullable: false),
                    ServicesId = table.Column<string>(type: "character varying(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandEntityServiceEntity", x => new { x.AutoRepairsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_BrandEntityServiceEntity_Brands_AutoRepairsId",
                        column: x => x.AutoRepairsId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_AutoServiceId_Name",
                table: "Services",
                columns: new[] { "AutoServiceId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_VenueId",
                table: "Services",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandEntityServiceEntity_ServicesId",
                table: "BrandEntityServiceEntity",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Brands_AutoServiceId",
                table: "Services",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Venues_VenueId",
                table: "Services",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
