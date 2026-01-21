using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class notemodelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Notes",
                newName: "CreatedDateTime");

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Notes",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoteName",
                table: "Notes",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Notes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Notes",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Notes",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteName",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "CreatedDateTime",
                table: "Notes",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notes",
                type: "VARCHAR(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
