﻿namespace WelcomeHome.Services.DTO;

public record TokensDTO
{
    public string JwtToken { get; init; }
    public string RefreshToken { get; init; }
}
