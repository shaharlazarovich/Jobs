using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddedAuditEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityName = table.Column<string>(nullable: true),
                    EntityId = table.Column<int>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    ModType = table.Column<string>(nullable: true),
                    RowAfter = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

        }
    }
}
