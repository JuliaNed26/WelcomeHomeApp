namespace WelcomeHome.Services.DTO
{
    public class UserLoginResponseDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? Role { get; set; }

        public string AccessToken { get; set; }
    }
}
