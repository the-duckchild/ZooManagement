using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeys9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_species_Classifications_ClassificationId",
                table: "species");

            migrationBuilder.AlterColumn<int>(
                name: "ClassificationId",
                table: "species",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "ClassificationId",
                table: "species",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_species_Classifications_ClassificationId",
                table: "species",
                column: "ClassificationId",
                principalTable: "Classifications",
                principalColumn: "Id");
        }
    }
}
