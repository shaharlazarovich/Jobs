using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedJobEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Replication = table.Column<string>(nullable: true),
                    Servers = table.Column<string>(nullable: true),
                    LastRun = table.Column<DateTime>(nullable: false),
                    RTA = table.Column<string>(nullable: false),
                    Results = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    RTONeeded = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
