using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedTrackEventEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "TrackEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    JobId = table.Column<string>(nullable: true),
                    JobName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Event = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    RemoteIP = table.Column<string>(nullable: true),
                    RemoteResponse = table.Column<string>(nullable: true),
                    RequestProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackEvents");

        }
    }
}
