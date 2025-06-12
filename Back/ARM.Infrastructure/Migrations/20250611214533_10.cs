using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AutoServices_AutoServiceId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AutoServices_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.DropTable(
                name: "AutoServiceEntityServiceEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_AutoServiceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AutoServiceId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BrandId",
                table: "Users",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VenueId",
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
                        name: "FK_BrandEntityServiceEntity_AutoServices_AutoRepairsId",
                        column: x => x.AutoRepairsId,
                        principalTable: "AutoServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Latitude = table.Column<double>(type: "double precision", precision: 10, scale: 6, nullable: false),
                    Longitude = table.Column<double>(type: "double precision", precision: 10, scale: 6, nullable: false),
                    BrandId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venues_AutoServices_BrandId",
                        column: x => x.BrandId,
                        principalTable: "AutoServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BrandId",
                table: "Users",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_VenueId",
                table: "Services",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandEntityServiceEntity_ServicesId",
                table: "BrandEntityServiceEntity",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_BrandId",
                table: "Venues",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_Latitude_Longitude",
                table: "Venues",
                columns: new[] { "Latitude", "Longitude" });

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Venues_VenueId",
                table: "Services",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AutoServices_BrandId",
                table: "Users",
                column: "BrandId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_AutoServices_AutoServiceId",
                table: "WorkingHours",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Venues_VenueId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AutoServices_BrandId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AutoServices_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.DropTable(
                name: "BrandEntityServiceEntity");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Users_BrandId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Services_VenueId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "AutoServiceId",
                table: "Users",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AutoServiceEntityServiceEntity",
                columns: table => new
                {
                    AutoRepairsId = table.Column<string>(type: "character varying(24)", nullable: false),
                    ServicesId = table.Column<string>(type: "character varying(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoServiceEntityServiceEntity", x => new { x.AutoRepairsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_AutoServiceEntityServiceEntity_AutoServices_AutoRepairsId",
                        column: x => x.AutoRepairsId,
                        principalTable: "AutoServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoServiceEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AutoServiceId",
                table: "Users",
                column: "AutoServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoServiceEntityServiceEntity_ServicesId",
                table: "AutoServiceEntityServiceEntity",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AutoServices_AutoServiceId",
                table: "Users",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_AutoServices_AutoServiceId",
                table: "WorkingHours",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
