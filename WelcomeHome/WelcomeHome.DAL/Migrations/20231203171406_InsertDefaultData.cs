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
                INSERT INTO Countries (Name) VALUES (N'Україна');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO dbo.EstablishmentTypes(Name) VALUES (N'Волонтерська організація');
            ");

            migrationBuilder.Sql(@"
                INSERT INTO Cities (Name, CountryId)
                VALUES (N'Київ', (SELECT TOP 1 Id FROM dbo.Countries WHERE Name = N'Україна'));
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
