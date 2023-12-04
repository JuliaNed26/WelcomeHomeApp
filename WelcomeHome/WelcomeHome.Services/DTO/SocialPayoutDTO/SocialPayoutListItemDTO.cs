namespace WelcomeHome.Services.DTO
{
    public class SocialPayoutListItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public ICollection<UserCategoryOutDTO>? UserCategories { get; set; }

    }
}
