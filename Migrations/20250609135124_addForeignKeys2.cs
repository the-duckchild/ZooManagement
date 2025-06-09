using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Classifications_ClassificationId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ClassificationId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "species",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_species_ClassificationId",
                table: "species",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_species_Classifications_ClassificationId",
                table: "species",
                column: "ClassificationId",
                principalTable: "Classifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_species_Classifications_ClassificationId",
                table: "species");

            migrationBuilder.DropIndex(
                name: "IX_species_ClassificationId",
                table: "species");

            migrationBuilder.DropColumn(
                name: "ClassificationId",
                table: "species");

            migrationBuilder.AddColumn<int>(
                name: "ClassificationId",
                table: "Animals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ClassificationId",
                table: "Animals",
                column: "ClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Classifications_ClassificationId",
                table: "Animals",
                column: "ClassificationId",
                principalTable: "Classifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
