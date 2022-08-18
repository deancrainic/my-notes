using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyNotes.Infrastructure.Migrations
{
    public partial class ChangeDateCreatedToLastEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Notes",
                newName: "LastEdited");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastEdited",
                table: "Notes",
                newName: "DateCreated");
        }
    }
}
