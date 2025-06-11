using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ARM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoRepairs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TotalReviews = table.Column<int>(type: "integer", nullable: false),
                    Specializations = table.Column<string>(type: "text", nullable: false),
                    MaxCarsPerDay = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    HasParking = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    HasWaitingRoom = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoRepairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlackListeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    AddedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    DeviceInfo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActiveSessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeviceInfo = table.Column<string>(type: "text", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActiveSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MakeOrderRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TechnicalPassport = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Make = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ProblemDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CarPlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    PreferredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AutoRepairId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakeOrderRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MakeOrderRequests_AutoRepairs_AutoRepairId",
                        column: x => x.AutoRepairId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    OpenTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AutoServiceId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_AutoRepairs_AutoServiceId",
                        column: x => x.AutoServiceId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    PermissionsId = table.Column<string>(type: "character varying(24)", nullable: false),
                    RolesId = table.Column<string>(type: "character varying(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ProfilePicture = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoleId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PermissionEntityId = table.Column<string>(type: "character varying(24)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Permissions_PermissionEntityId",
                        column: x => x.PermissionEntityId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AutoServiceId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    ServiceTypeId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_AutoRepairs_AutoServiceId",
                        column: x => x.AutoServiceId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Make = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CarPlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    VIN = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    EngineType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    EngineVolume = table.Column<double>(type: "double precision", precision: 3, scale: 1, nullable: false),
                    Transmission = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    OwnerId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    AutoServiceId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AutoRepairs_AutoServiceId",
                        column: x => x.AutoServiceId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_AutoServiceEntityServiceEntity_AutoRepairs_AutoRepairsId",
                        column: x => x.AutoRepairsId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoServiceEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CarId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    AutoServiceId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    RepairDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Diagnosis = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    WorkPerformed = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    PartsReplaced = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Recommendations = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    PartsCost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    LaborCost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Mileage = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairLogs_AutoRepairs_AutoServiceId",
                        column: x => x.AutoServiceId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairLogs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairOrders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    UserId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    CarId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    AutoServiceId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    ServiceTypeId = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstimatedDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DiagnosisResults = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    ActualCost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CancellationReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ServiceStatus = table.Column<string>(type: "text", nullable: false),
                    EstimatedCost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CustomerComments = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairOrders_AutoRepairs_AutoServiceId",
                        column: x => x.AutoServiceId,
                        principalTable: "AutoRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrders_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairLogEntityServiceEntity",
                columns: table => new
                {
                    RepairLogEntityId = table.Column<string>(type: "character varying(24)", nullable: false),
                    ServicesId = table.Column<string>(type: "character varying(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairLogEntityServiceEntity", x => new { x.RepairLogEntityId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_RepairLogEntityServiceEntity_RepairLogs_RepairLogEntityId",
                        column: x => x.RepairLogEntityId,
                        principalTable: "RepairLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairLogEntityServiceEntity_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoRepairs_CreatedAt",
                table: "AutoRepairs",
                column: "CreatedAt");

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

            migrationBuilder.CreateIndex(
                name: "IX_AutoServiceEntityServiceEntity_ServicesId",
                table: "AutoServiceEntityServiceEntity",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarPlate",
                table: "Cars",
                column: "CarPlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_VIN",
                table: "Cars",
                column: "VIN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MakeOrderRequests_AutoRepairId",
                table: "MakeOrderRequests",
                column: "AutoRepairId");

            migrationBuilder.CreateIndex(
                name: "IX_MakeOrderRequests_RequestDate",
                table: "MakeOrderRequests",
                column: "RequestDate");

            migrationBuilder.CreateIndex(
                name: "IX_MakeOrderRequests_Status",
                table: "MakeOrderRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogEntityServiceEntity_ServicesId",
                table: "RepairLogEntityServiceEntity",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogs_AutoServiceId",
                table: "RepairLogs",
                column: "AutoServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogs_CarId_RepairDate",
                table: "RepairLogs",
                columns: new[] { "CarId", "RepairDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RepairLogs_RepairDate",
                table: "RepairLogs",
                column: "RepairDate");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_AutoServiceId",
                table: "RepairOrders",
                column: "AutoServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_CarId",
                table: "RepairOrders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_OrderDate",
                table: "RepairOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_ScheduledDate",
                table: "RepairOrders",
                column: "ScheduledDate");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_ServiceStatus",
                table: "RepairOrders",
                column: "ServiceStatus");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_ServiceTypeId",
                table: "RepairOrders",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_UserId_ServiceStatus",
                table: "RepairOrders",
                columns: new[] { "UserId", "ServiceStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AutoServiceId",
                table: "Reviews",
                column: "AutoServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RolesId",
                table: "RolePermissions",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_AutoServiceId_Name",
                table: "Services",
                columns: new[] { "AutoServiceId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Price",
                table: "Services",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceTypeId",
                table: "Services",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_Category",
                table: "ServiceTypes",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_Name",
                table: "ServiceTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name_Surname",
                table: "Users",
                columns: new[] { "Name", "Surname" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionEntityId",
                table: "Users",
                column: "PermissionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_AutoServiceId_Day",
                table: "WorkingHours",
                columns: new[] { "AutoServiceId", "Day" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoServiceEntityServiceEntity");

            migrationBuilder.DropTable(
                name: "BlackListeds");

            migrationBuilder.DropTable(
                name: "MakeOrderRequests");

            migrationBuilder.DropTable(
                name: "RepairLogEntityServiceEntity");

            migrationBuilder.DropTable(
                name: "RepairOrders");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserActiveSessions");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "RepairLogs");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "AutoRepairs");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
