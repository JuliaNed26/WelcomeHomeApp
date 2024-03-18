using FluentValidation;
using WelcomeHome.Services.DTO.VacancyDTO;
using WelcomeHome.Services.Exceptions;
using WelcomeHome.Services.Validators.PaginationDto;

namespace WelcomeHome.Services.Validators;

internal static class ValidatorFactory
{
    public static object GetValidatorByType(object dto)
    {
        return dto switch
        {
            PaginationOptionsDTO => new PaginationDtoValidator(),
            _ => throw new BusinessException("Do not have validator for such type"),
        };
    }
}
