using FluentValidation;
using WelcomeHome.Services.DTO.VacancyDTO;

namespace WelcomeHome.Services.Validators.PaginationDto;

internal class PaginationDtoValidator : AbstractValidator<PaginationOptionsDTO>
{
    public PaginationDtoValidator()
    {
        RuleFor(p => p.CountOnPage).GreaterThan(0);
        RuleFor(p => p.PageNumber).GreaterThan(0);
    }
}
