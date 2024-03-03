using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class VacanciesWithVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VolunteerId",
                table: "Vacancies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_VolunteerId",
                table: "Vacancies",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Volunteers_VolunteerId",
                table: "Vacancies",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Volunteers_VolunteerId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_VolunteerId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Vacancies");
        }
    }
}
