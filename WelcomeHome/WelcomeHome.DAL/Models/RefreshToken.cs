namespace WelcomeHome.DAL.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
