﻿using System.ComponentModel.DataAnnotations;

namespace WelcomeHome.DAL.Models
{
	public class SocialPayout
	{
		[Key]
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public double Amount { get; set; }

		public ICollection<UserCategory>? UserCategories { get; set; }

		public ICollection<PaymentStep> PaymentSteps { get; set; }
	}
}
