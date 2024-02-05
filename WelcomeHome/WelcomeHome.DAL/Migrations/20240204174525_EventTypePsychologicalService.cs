using Microsoft.EntityFrameworkCore.Migrations;
using WelcomeHome.DAL.EventTypeNameRetriever;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EventTypePsychologicalService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO EventTypes (Name)"
                +$"VALUES (N'{EventTypeNames.PsychologicalService}');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
