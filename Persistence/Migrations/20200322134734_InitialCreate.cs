using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EDRM");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(nullable: true),
                    EntityId = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    ModType = table.Column<string>(nullable: true),
                    RowAfter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobActions",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobId = table.Column<long>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    RemoteIP = table.Column<string>(nullable: true),
                    RemoteResponse = table.Column<string>(nullable: true),
                    RequestProperties = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Replication = table.Column<string>(nullable: true),
                    Servers = table.Column<string>(nullable: true),
                    LastRun = table.Column<DateTime>(nullable: false),
                    RTA = table.Column<string>(nullable: true),
                    Results = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    RTONeeded = table.Column<string>(nullable: true),
                    JobIP = table.Column<string>(nullable: true),
                    jobStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "EDRM",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "EDRM",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "EDRM",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "EDRM",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "EDRM",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "EDRM",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "EDRM",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "EDRM",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "EDRM",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceName = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    DeviceIP = table.Column<string>(nullable: true),
                    DeviceUser = table.Column<string>(nullable: true),
                    DevicePassword = table.Column<string>(nullable: true),
                    DeviceGroup = table.Column<string>(nullable: true),
                    JobId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "EDRM",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobSummaryResults",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobId = table.Column<long>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    DataMover = table.Column<string>(nullable: true),
                    RealDowntime = table.Column<string>(nullable: true),
                    JobStatus = table.Column<string>(nullable: true),
                    ServersInStatusOK = table.Column<long>(nullable: false),
                    ServersInStatusBAD = table.Column<long>(nullable: false),
                    RunDate = table.Column<DateTime>(nullable: false),
                    NextRun = table.Column<DateTime>(nullable: false),
                    PreTestTask = table.Column<bool>(nullable: false),
                    FailOver = table.Column<bool>(nullable: false),
                    ServerTest = table.Column<bool>(nullable: false),
                    SnapShotVMs = table.Column<bool>(nullable: false),
                    CleanupFailOver = table.Column<bool>(nullable: false),
                    GlobalSiteMap = table.Column<string>(nullable: true),
                    NumberOfVMs = table.Column<long>(nullable: false),
                    NumberOfPhisicals = table.Column<long>(nullable: false),
                    NumberOfDevices = table.Column<long>(nullable: false),
                    RTA = table.Column<decimal>(nullable: false),
                    RTO = table.Column<decimal>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    PreviousScore = table.Column<int>(nullable: false),
                    iopsCapacity = table.Column<decimal>(nullable: false),
                    iopsActual = table.Column<decimal>(nullable: false),
                    ramCapacity = table.Column<decimal>(nullable: false),
                    ramActual = table.Column<decimal>(nullable: false),
                    cpuCapacity = table.Column<decimal>(nullable: false),
                    cpuActual = table.Column<decimal>(nullable: false),
                    KnownIssues = table.Column<string>(nullable: true),
                    SummaryOverTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSummaryResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSummaryResults_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "EDRM",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DRTests",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DeviceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DRTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DRTests_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalSchema: "EDRM",
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobDeviceResults",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobId = table.Column<long>(nullable: false),
                    JobSummaryResultId = table.Column<long>(nullable: false),
                    machineName = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    results = table.Column<string>(nullable: true),
                    testGroup = table.Column<int>(nullable: false),
                    TargetServer = table.Column<string>(nullable: true),
                    JobHealthBeforeTest = table.Column<string>(nullable: true),
                    HostServer = table.Column<string>(nullable: true),
                    HostServerType = table.Column<string>(nullable: true),
                    UndoTestFailOverStatus = table.Column<string>(nullable: true),
                    AutoTroubleshooting = table.Column<string>(nullable: true),
                    smallGif = table.Column<string>(nullable: true),
                    largeGif = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDeviceResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDeviceResults_JobSummaryResults_JobSummaryResultId",
                        column: x => x.JobSummaryResultId,
                        principalSchema: "EDRM",
                        principalTable: "JobSummaryResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobTestResults",
                schema: "EDRM",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobId = table.Column<long>(nullable: false),
                    JobDeviceResultId = table.Column<long>(nullable: false),
                    testType = table.Column<long>(nullable: false),
                    testResult = table.Column<string>(nullable: true),
                    mediaURL = table.Column<string>(nullable: true),
                    TestStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobTestResults_JobDeviceResults_JobDeviceResultId",
                        column: x => x.JobDeviceResultId,
                        principalSchema: "EDRM",
                        principalTable: "JobDeviceResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "EDRM",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "EDRM",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "EDRM",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "EDRM",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "EDRM",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "EDRM",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "EDRM",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audits_Time",
                schema: "EDRM",
                table: "Audits",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_EntityName_EntityId",
                schema: "EDRM",
                table: "Audits",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Audits_EntityName_Time",
                schema: "EDRM",
                table: "Audits",
                columns: new[] { "EntityName", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_JobId",
                schema: "EDRM",
                table: "Devices",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_DRTests_DeviceId",
                schema: "EDRM",
                table: "DRTests",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDeviceResults_JobSummaryResultId",
                schema: "EDRM",
                table: "JobDeviceResults",
                column: "JobSummaryResultId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSummaryResults_JobId",
                schema: "EDRM",
                table: "JobSummaryResults",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTestResults_JobDeviceResultId",
                schema: "EDRM",
                table: "JobTestResults",
                column: "JobDeviceResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "Audits",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "DRTests",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "JobActions",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "JobTestResults",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "Values",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "Devices",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "JobDeviceResults",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "JobSummaryResults",
                schema: "EDRM");

            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "EDRM");
        }
    }
}
