namespace WelcomeHome.DAL.Dto;
public record PaginationOptionsDto
{
    public int PageNumber { get; init; }

    public int CountOnPage { get; init; }
}
