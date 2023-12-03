using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                INSERT INTO AspNetRoles(Name, NormalizedName, ConcurrencyStamp)
                VALUES ('volunteer', 'VOLUNTEER', null)
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Countries (Name) VALUES ('Ukraine');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO dbo.EstablishmentTypes(Name) VALUES ('Волонтерська організація');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Cities (Name, CountryId)
                VALUES ('Kyiv', (SELECT TOP 1 Id FROM dbo.Countries WHERE Name = 'Ukraine'));
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
