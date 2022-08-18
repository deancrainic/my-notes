using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyNotes.Infrastructure.Migrations
{
    public partial class AddTitleToNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
