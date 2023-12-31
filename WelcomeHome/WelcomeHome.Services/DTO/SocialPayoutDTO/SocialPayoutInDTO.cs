﻿namespace WelcomeHome.Services.DTO
{
    public class SocialPayoutInDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public ICollection<int> UserCategoriesId { get; set; }

        public ICollection<StepInDTO>? NewPaymentSteps { get; set; }

        public ICollection<ExistingStepInDTO>? ExistingPaymentSteps { get; set; }
    }
}