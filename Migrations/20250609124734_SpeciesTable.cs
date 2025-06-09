using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Migrations
{
    /// <inheritdoc />
    public partial class SpeciesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Species",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeciesId",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SpeciesId",
                table: "Animals");

            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Animals",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
