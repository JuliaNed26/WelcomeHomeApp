using WelcomeHome.Services.DTO;

public class SocialPayoutOutDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }

    public ICollection<UserCategoryOutDTO>? UserCategories { get; set; }

    public ICollection<StepOutDTO>? Steps { get; set; }
}