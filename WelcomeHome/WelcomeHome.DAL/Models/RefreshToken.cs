﻿namespace WelcomeHome.DAL.Models;

public class RefreshToken
{
    public long Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
