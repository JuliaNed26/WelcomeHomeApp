using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PsychologicalTsenterToEstablishmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO dbo.EstablishmentTypes(Name) VALUES (N'Психологічний центр');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
