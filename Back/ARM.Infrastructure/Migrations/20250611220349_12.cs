using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandEntityServiceEntity_AutoServices_AutoRepairsId",
                table: "BrandEntityServiceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MakeOrderRequests_AutoServices_AutoRepairId",
                table: "MakeOrderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_AutoServices_AutoServiceId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOrders_AutoServices_AutoServiceId",
                table: "RepairOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AutoServices_AutoServiceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AutoServices_AutoServiceId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AutoServices_BrandId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_AutoServices_BrandId",
                table: "Venues");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AutoServices_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutoServices",
                table: "AutoServices");

            migrationBuilder.RenameTable(
                name: "AutoServices",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_AutoServices_PhoneNumber",
                table: "Brands",
                newName: "IX_Brands_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AutoServices_Name",
                table: "Brands",
                newName: "IX_Brands_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AutoServices_Latitude_Longitude",
                table: "Brands",
                newName: "IX_Brands_Latitude_Longitude");

            migrationBuilder.RenameIndex(
                name: "IX_AutoServices_Email",
                table: "Brands",
                newName: "IX_Brands_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandEntityServiceEntity_Brands_AutoRepairsId",
                table: "BrandEntityServiceEntity",
                column: "AutoRepairsId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MakeOrderRequests_Brands_AutoRepairId",
                table: "MakeOrderRequests",
                column: "AutoRepairId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_Brands_AutoServiceId",
                table: "RepairLogs",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOrders_Brands_AutoServiceId",
                table: "RepairOrders",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Brands_AutoServiceId",
                table: "Reviews",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Brands_AutoServiceId",
                table: "Services",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Brands_BrandId",
                table: "Users",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Brands_BrandId",
                table: "Venues",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_Brands_AutoServiceId",
                table: "WorkingHours",
                column: "AutoServiceId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandEntityServiceEntity_Brands_AutoRepairsId",
                table: "BrandEntityServiceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MakeOrderRequests_Brands_AutoRepairId",
                table: "MakeOrderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_Brands_AutoServiceId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOrders_Brands_AutoServiceId",
                table: "RepairOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Brands_AutoServiceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Brands_AutoServiceId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Brands_BrandId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Brands_BrandId",
                table: "Venues");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_Brands_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "AutoServices");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_PhoneNumber",
                table: "AutoServices",
                newName: "IX_AutoServices_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_Name",
                table: "AutoServices",
                newName: "IX_AutoServices_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_Latitude_Longitude",
                table: "AutoServices",
                newName: "IX_AutoServices_Latitude_Longitude");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_Email",
                table: "AutoServices",
                newName: "IX_AutoServices_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutoServices",
                table: "AutoServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandEntityServiceEntity_AutoServices_AutoRepairsId",
                table: "BrandEntityServiceEntity",
                column: "AutoRepairsId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MakeOrderRequests_AutoServices_AutoRepairId",
                table: "MakeOrderRequests",
                column: "AutoRepairId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_AutoServices_AutoServiceId",
                table: "RepairLogs",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOrders_AutoServices_AutoServiceId",
                table: "RepairOrders",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AutoServices_AutoServiceId",
                table: "Reviews",
                column: "AutoServiceId",
                principalTable: "AutoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AutoServices_AutoServiceId",
                table: "Services",
                column: "AutoServiceId",
                principalTable: "AutoServices",
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
                name: "FK_Venues_AutoServices_BrandId",
                table: "Venues",
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
    }
}
