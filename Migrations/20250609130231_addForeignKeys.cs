using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Animals_ClassificationId",
                table: "Animals",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_SpeciesId",
                table: "Animals",
                column: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Classifications_ClassificationId",
                table: "Animals",
                column: "ClassificationId",
                principalTable: "Classifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_species_SpeciesId",
                table: "Animals",
                column: "SpeciesId",
                principalTable: "species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Classifications_ClassificationId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_species_SpeciesId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ClassificationId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_SpeciesId",
                table: "Animals");
        }
    }
}
