using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class VacancyModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Establishments_EstablishmentId",
                table: "Vacancies");

            migrationBuilder.RenameColumn(
                name: "EstablishmentId",
                table: "Vacancies",
                newName: "CityId");

            migrationBuilder.AlterColumn<string>(
                name: "PageURL",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Cities_CityId",
                table: "Vacancies",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Cities_CityId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Vacancies");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Vacancies",
                newName: "EstablishmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_CityId",
                table: "Vacancies",
                newName: "IX_Vacancies_EstablishmentId");

            migrationBuilder.AlterColumn<string>(
                name: "PageURL",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Establishments_EstablishmentId",
                table: "Vacancies",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
