using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoServiceEntityServiceEntity_AutoRepairs_AutoRepairsId",
                table: "AutoServiceEntityServiceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MakeOrderRequests_AutoRepairs_AutoRepairId",
                table: "MakeOrderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairLogs_AutoRepairs_AutoServiceId",
                table: "RepairLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOrders_AutoRepairs_AutoServiceId",
                table: "RepairOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AutoRepairs_AutoServiceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AutoRepairs_AutoServiceId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AutoRepairs_AutoServiceId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AutoRepairs_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutoRepairs",
                table: "AutoRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AutoRepairs_Email",
                table: "AutoRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AutoRepairs_Name",
                table: "AutoRepairs");

            migrationBuilder.DropIndex(
                name: "IX_AutoRepairs_PhoneNumber",
                table: "AutoRepairs");

            migrationBuilder.RenameTable(
                name: "AutoRepairs",
                newName: "AutoServices");

            migrationBuilder.RenameIndex(
                name: "IX_AutoRepairs_CreatedAt",
                table: "AutoServices",
                newName: "IX_AutoServices_CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AutoServices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AutoServices",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AutoServices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "AutoServices",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AutoServices",
                type: "double precision",
                precision: 10,
                scale: 6,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AutoServices",
                type: "double precision",
                precision: 10,
                scale: 6,
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutoServices",
                table: "AutoServices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AutoServices_Email",
                table: "AutoServices",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoServices_Latitude_Longitude",
                table: "AutoServices",
                columns: new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_AutoServices_Name",
                table: "AutoServices",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoServices_PhoneNumber",
                table: "AutoServices",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AutoServiceEntityServiceEntity_AutoServices_AutoRepairsId",
                table: "AutoServiceEntityServiceEntity",
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
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutoServiceEntityServiceEntity_AutoServices_AutoRepairsId",
                table: "AutoServiceEntityServiceEntity");

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
                name: "FK_Users_AutoServices_AutoServiceId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingHours_AutoServices_AutoServiceId",
                table: "WorkingHours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AutoServices",
                table: "AutoServices");

            migrationBuilder.DropIndex(
                name: "IX_AutoServices_Email",
                table: "AutoServices");

            migrationBuilder.DropIndex(
                name: "IX_AutoServices_Latitude_Longitude",
                table: "AutoServices");

            migrationBuilder.DropIndex(
                name: "IX_AutoServices_Name",
                table: "AutoServices");

            migrationBuilder.DropIndex(
                name: "IX_AutoServices_PhoneNumber",
                table: "AutoServices");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AutoServices");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "AutoServices");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AutoServices");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AutoServices");

            migrationBuilder.RenameTable(
                name: "AutoServices",
                newName: "AutoRepairs");

            migrationBuilder.RenameIndex(
                name: "IX_AutoServices_CreatedAt",
                table: "AutoRepairs",
                newName: "IX_AutoRepairs_CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AutoRepairs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AutoRepairs",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AutoRepairs",
                table: "AutoRepairs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRepairs_Email",
                table: "AutoRepairs",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRepairs_Name",
                table: "AutoRepairs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AutoRepairs_PhoneNumber",
                table: "AutoRepairs",
                column: "PhoneNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_AutoServiceEntityServiceEntity_AutoRepairs_AutoRepairsId",
                table: "AutoServiceEntityServiceEntity",
                column: "AutoRepairsId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MakeOrderRequests_AutoRepairs_AutoRepairId",
                table: "MakeOrderRequests",
                column: "AutoRepairId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairLogs_AutoRepairs_AutoServiceId",
                table: "RepairLogs",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOrders_AutoRepairs_AutoServiceId",
                table: "RepairOrders",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AutoRepairs_AutoServiceId",
                table: "Reviews",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AutoRepairs_AutoServiceId",
                table: "Services",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AutoRepairs_AutoServiceId",
                table: "Users",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingHours_AutoRepairs_AutoServiceId",
                table: "WorkingHours",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
