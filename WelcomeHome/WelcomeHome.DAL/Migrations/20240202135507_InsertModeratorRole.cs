using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InsertModeratorRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO AspNetRoles(Name, NormalizedName, ConcurrencyStamp)
                VALUES ('moderator', 'MODERATOR', null)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
