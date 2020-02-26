using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedJobActionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "JobActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    JobId = table.Column<string>(nullable: true),
                    JobName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    RemoteIP = table.Column<string>(nullable: true),
                    RemoteResponse = table.Column<string>(nullable: true),
                    RequestProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobActions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobActions");

        }
    }
}
