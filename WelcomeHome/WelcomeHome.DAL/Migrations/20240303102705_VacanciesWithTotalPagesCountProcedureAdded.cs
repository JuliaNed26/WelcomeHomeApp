using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelcomeHome.DAL.Migrations
{
    /// <inheritdoc />
    public partial class VacanciesWithTotalPagesCountProcedureAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetVacancyPageWithTotalVacanciesCount
                                   	    @page int = 1,
										@countOnPage int = 20
									AS
									BEGIN
										SELECT ROUND((COUNT(*) OVER ()) / @countOnPage, 0) AS TotalPagesCount,
										       Vacancies.Id,
											   Vacancies.Name,
											   Vacancies.CompanyName,
											   Vacancies.Description, 
											   Vacancies.Salary,
											   Vacancies.PageURL,
											   Vacancies.PhoneNumber,
											   Vacancies.OtherContacts,
											   Vacancies.CityId,
											   Cities.Name As CityName
										FROM Vacancies
										     LEFT JOIN Cities ON Vacancies.CityId = Cities.Id
										ORDER BY Vacancies.Id
										OFFSET (@page - 1) * @countOnPage ROWS
										FETCH NEXT @countOnPage ROWS ONLY;
									END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
