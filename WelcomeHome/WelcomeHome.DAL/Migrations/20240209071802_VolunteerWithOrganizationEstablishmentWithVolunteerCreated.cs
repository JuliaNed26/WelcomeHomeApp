using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerWithOrganizationEstablishmentWithVolunteerCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_Establishments_EstablishmentId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_EstablishmentId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "EstablishmentId",
                table: "Volunteers");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "Volunteers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "Establishments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L); 

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_OrganizationId",
                table: "Volunteers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Establishments_CreatorId",
                table: "Establishments",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Volunteers_CreatorId",
                table: "Establishments",
                column: "CreatorId",
                principalTable: "Volunteers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Establishments_OrganizationId",
                table: "Volunteers",
                column: "OrganizationId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Volunteers_CreatorId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_Establishments_OrganizationId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_OrganizationId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Establishments_CreatorId",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Establishments");

            migrationBuilder.AddColumn<long>(
                name: "EstablishmentId",
                table: "Volunteers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_EstablishmentId",
                table: "Volunteers",
                column: "EstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Establishments_EstablishmentId",
                table: "Volunteers",
                column: "EstablishmentId",
                principalTable: "Establishments",
                principalColumn: "Id");
        }
    }
}
