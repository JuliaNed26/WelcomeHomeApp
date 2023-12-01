namespace WelcomeHome.Services.DTO;

public record TokensDto
{
    public string JwtToken { get; init; }
    public string RefreshToken { get; init; }
}
