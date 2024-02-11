using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_Volunteers_CreatorId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_Establishments_OrganizationId",
                table: "Volunteers");

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "Volunteers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Volunteers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Volunteers_CreatorId",
                table: "Establishments",
                column: "CreatorId",
                principalTable: "Volunteers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Establishments_OrganizationId",
                table: "Volunteers",
                column: "OrganizationId",
                principalTable: "Establishments",
                principalColumn: "Id");
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

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Volunteers");

            migrationBuilder.AlterColumn<long>(
                name: "OrganizationId",
                table: "Volunteers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_Volunteers_CreatorId",
                table: "Establishments",
                column: "CreatorId",
                principalTable: "Volunteers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_Establishments_OrganizationId",
                table: "Volunteers",
                column: "OrganizationId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
