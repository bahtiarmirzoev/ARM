using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutoServiceId",
                table: "Users",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Services",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ServiceId",
                table: "MakeOrderRequests",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MakeOrderRequests",
                type: "character varying(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "AutoRepairs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "AutoRepairs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AutoServiceId",
                table: "Users",
                column: "AutoServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Rating",
                table: "Services",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_MakeOrderRequests_ServiceId",
                table: "MakeOrderRequests",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MakeOrderRequests_UserId",
                table: "MakeOrderRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MakeOrderRequests_Services_ServiceId",
                table: "MakeOrderRequests",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MakeOrderRequests_Users_UserId",
                table: "MakeOrderRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AutoRepairs_AutoServiceId",
                table: "Users",
                column: "AutoServiceId",
                principalTable: "AutoRepairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakeOrderRequests_Services_ServiceId",
                table: "MakeOrderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MakeOrderRequests_Users_UserId",
                table: "MakeOrderRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AutoRepairs_AutoServiceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AutoServiceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Services_Rating",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_MakeOrderRequests_ServiceId",
                table: "MakeOrderRequests");

            migrationBuilder.DropIndex(
                name: "IX_MakeOrderRequests_UserId",
                table: "MakeOrderRequests");

            migrationBuilder.DropColumn(
                name: "AutoServiceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "MakeOrderRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MakeOrderRequests");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AutoRepairs");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "AutoRepairs");
        }
    }
}
